using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //int som bruges til den der LoadNextScene funktion
    public int nextScene;
    public int menuScene;
    bool isLoading;
    private static GameManager instance;

    [Header("References")]
    public GameObject bottomBar;

    public void Start()
    {
        instance = this;
    }

    public void OnEnable()
    {
        SceneManager.activeSceneChanged -= OnSceneActivated;
        SceneManager.activeSceneChanged += OnSceneActivated;
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {

                instance = (Instantiate(Resources.Load("GameManager")) as GameObject).GetComponent<GameManager>();
            }
            return instance;
        }
    }
    public void LoadNextScene()

    {
        Debug.Log("button clicked");
        if (!isLoading)
        {


            //henter den nuværende scene, og tilføjer 1 til buildindexet. Så sender den denne int med ind i coroutinen, som loader scener
            nextScene = SceneManager.GetActiveScene().buildIndex;

            nextScene += 1;
            Debug.Log("button clicked");

            //Hvis den bliver bedt om at loade en scene, som er uden for buildindexet, loader den bare menuen.
            if (nextScene > (SceneManager.sceneCountInBuildSettings - 1))
            {
                nextScene = menuScene;
            }

            StartCoroutine(UnloadSceneToLoadSpecificLevel(nextScene));
        }
    }


    //Tager en int ind, og sætter gang i coroutinen som loader et level, og unloader det nuværende
    public void LoadSpecificScene(int buildIndexOfSceneToLoad)
    {
        StartCoroutine(UnloadSceneToLoadSpecificLevel(buildIndexOfSceneToLoad));
    }

    public void ReloadScene()
    {
        if (!isLoading)
        {


            //Sætter nextScene lig med den nuværende scene, unloader den nuværende, og loader derefter den nuværende, vha. coroutinen
            nextScene = SceneManager.GetActiveScene().buildIndex;
            if (SceneManager.GetSceneByBuildIndex(nextScene).isLoaded)
            {
                StartCoroutine(UnloadSceneToLoadSpecificLevel(nextScene));
            }
        }

    }

    IEnumerator LoadSceneAsync(int i)
    {
        if (!isLoading)
        {


            //funktionen som er ansvarlige for at loade alle scenerne. 
            //Den loader scenen ind additivt, og når den er færdig, sætter den scenen til at være den aktive.
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
            isLoading = true;
            while (!loadScene.isDone)
            {

                yield return null;
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(i));
            isLoading = false;
        }
    }

    public IEnumerator UnloadSceneToLoadSpecificLevel(int i)
    {
        if (!isLoading)
        {
            //Tjekker lige om det er menuen man har tænkt sig at unloade (det må man nemlig ikke)
            if (SceneManager.GetActiveScene().buildIndex != menuScene && SceneManager.sceneCount > 1)
            {
                //Hvis det ikke er menuen, giver vi os i kast med at unloade den nuværende scene. 
                AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                while (!unloadScene.isDone)
                {

                    yield return null;
                }
            }
            //Når vi er færdige med at unloade den nuværende scene, tjekker vi om vi har tænkt os at loade menuen. 
            //Vi gider nemlig ikke loade menuen, siden den hele tiden er aktiv (i og med, at vi ikke må unloade den).
            //Hvis vi på noget tidspunkt loader den, får vi flere aktive menu-scener. Det er dumt og dårligt. 
            //MEN, hvis det ikke er menuen, så sætter vi gang i coroutinen som additivt loader en ny scene.
            int current = SceneManager.GetActiveScene().buildIndex;
            if (i != menuScene && i != current)
            {

                StartCoroutine(LoadSceneAsync(i));
            }
            Debug.Log("Load");
        }

    }


    public void OnSceneActivated(Scene currentScene, Scene nextScene)
    {
        if (currentScene.IsValid())
        {
            GameObject[] currentSceneObjects = currentScene.GetRootGameObjects();

            /*
            for (int i = 0; i < currentSceneObjects.Length; i++)
            {
                UIController controller = currentSceneObjects[i].GetComponentInChildren<UIController>();
                if (controller != null)
                {
                    controller.DisableUI();
                    break;
                }
            }
            */
        }


        if (nextScene.IsValid())
        {
            GameObject[] currentSceneObjects = nextScene.GetRootGameObjects();
            /*
            for (int i = 0; i < currentSceneObjects.Length; i++)
            {
                UIController controller = currentSceneObjects[i].GetComponentInChildren<UIController>();
                if (controller != null)
                {
                    controller.EnableUI();
                }
            }
            */
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void SetBottomBar(bool state)
    {
        if(state == false)
        {
            bottomBar.SetActive(false);
        }
        else
        {
            bottomBar.SetActive(true);
        }
    }


}

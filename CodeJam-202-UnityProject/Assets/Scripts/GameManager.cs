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


            //henter den nuv�rende scene, og tilf�jer 1 til buildindexet. S� sender den denne int med ind i coroutinen, som loader scener
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


    //Tager en int ind, og s�tter gang i coroutinen som loader et level, og unloader det nuv�rende
    public void LoadSpecificScene(int buildIndexOfSceneToLoad)
    {
        StartCoroutine(UnloadSceneToLoadSpecificLevel(buildIndexOfSceneToLoad));
    }

    public void ReloadScene()
    {
        if (!isLoading)
        {


            //S�tter nextScene lig med den nuv�rende scene, unloader den nuv�rende, og loader derefter den nuv�rende, vha. coroutinen
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
            //Den loader scenen ind additivt, og n�r den er f�rdig, s�tter den scenen til at v�re den aktive.
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
            //Tjekker lige om det er menuen man har t�nkt sig at unloade (det m� man nemlig ikke)
            if (SceneManager.GetActiveScene().buildIndex != menuScene && SceneManager.sceneCount > 1)
            {
                //Hvis det ikke er menuen, giver vi os i kast med at unloade den nuv�rende scene. 
                AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                while (!unloadScene.isDone)
                {

                    yield return null;
                }
            }
            //N�r vi er f�rdige med at unloade den nuv�rende scene, tjekker vi om vi har t�nkt os at loade menuen. 
            //Vi gider nemlig ikke loade menuen, siden den hele tiden er aktiv (i og med, at vi ikke m� unloade den).
            //Hvis vi p� noget tidspunkt loader den, f�r vi flere aktive menu-scener. Det er dumt og d�rligt. 
            //MEN, hvis det ikke er menuen, s� s�tter vi gang i coroutinen som additivt loader en ny scene.
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetChildrenToActive : MonoBehaviour
{
    public List<GameObject> children;
    public int desiredBuildIndex;

    //Denne funktion bliver kaldt når UI elementerne fra menuen skal skjules, da vi skal til at loade en scene der ikke er menuen
    public void TurnOffUI()
    {

        foreach (GameObject child in children)
        {
            if (SceneManager.GetActiveScene().buildIndex == desiredBuildIndex)
            {
                child.SetActive(true);
            }
            else
            {
                child.SetActive(false);
                Debug.Log("Disappear");
                desiredBuildIndex = 0;
            }

        }

    }

}

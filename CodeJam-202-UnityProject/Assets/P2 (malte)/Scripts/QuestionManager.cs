using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    //Static for singleton script
    public static QuestionManager instance;

    //Set public gameobjects to be called later
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;


    //Checks if this script already exists, so there will always only be one 
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //Method for changing between the pages, activates and deactivates gameobjects
    public void NextPage()
    {
        if (one.activeInHierarchy)
        {
            one.SetActive(false);
            two.SetActive(true);
        }
        else if(two.activeInHierarchy)
        {
            two.SetActive(false);
            three.SetActive(true);
        }
        else if (three.activeInHierarchy)
        {
            three.SetActive(false);
            four.SetActive(true);
        }
        else
        {
            //On the last page we switch over to the feed scene and evaluate all events to sort the list of events for the feed.
            //GameManager.Instance.SetBottomBar(true);
            MoroEventManager.instance.EvalAllEvents();
            GameManager.Instance.LoadNextScene();
        }
    }

}

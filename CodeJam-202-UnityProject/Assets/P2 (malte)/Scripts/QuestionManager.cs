using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager instance;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;

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

        }
    }

}

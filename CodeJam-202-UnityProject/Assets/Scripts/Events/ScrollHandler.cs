using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHandler : MonoBehaviour
{
    //Singleton reference.
    public static ScrollHandler instance;

    //Reference to the scrollview's scrollrect. Set in the inspector.
    public ScrollRect sr; 

    //Singleton logic.
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
    
    // Update is called once per frame
    void Update()
    {
        //This just checks if the user is trying to scroll past the top, and then stops them from doing so.
        if(sr.verticalNormalizedPosition > 1)
        {
            sr.verticalNormalizedPosition = 1;
        }
    }

    //Just returns how far down the user have scrolled in pixels. Used by the event stack to check out of bounds as well as elements to figure out if they're on screen or not.
    public float GetScrollPos()
    {
        float pos = sr.content.anchoredPosition.y;
        return pos;
    }
}

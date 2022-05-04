using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHandler : MonoBehaviour
{
    public static ScrollHandler instance;

    public ScrollRect sr;

    float elementHeight = -1;

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

    private void Start()
    {

        elementHeight = 1000; //desiredHeight + gap;
        Debug.Log("Element Height: " + elementHeight);
        Debug.Log("Viewport thing: " + sr.viewport.sizeDelta.y);
        sr.content.sizeDelta = new Vector2(0, sr.GetComponent<RectTransform>().sizeDelta.y + elementHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if(sr.verticalNormalizedPosition > 1)
        {
            sr.verticalNormalizedPosition = 1;
        }
    }

    public float GetScrollPos()
    {
        float pos = sr.content.anchoredPosition.y;
        return pos;
    }
}

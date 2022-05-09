using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script only exists to give accurate screen size. This is because some devices run a lower resolution than their actual screen size.
//Which means that Screen.height can report a value inconsistent with the size of the canvas in the application.
public class CanvasRef : MonoBehaviour
{
    //Singleton reference
    public static CanvasRef instance;

    //Reference to the canvas.
    public RectTransform canvas;


    //Singleton logic
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //Reports canvas height. Alternative to Screen.height that reports the correct value when device resolution doesn't match device size.
    public float GetCanvasHeight()
    {
        float c;
        c = canvas.sizeDelta.y;
        return c;
    }

    //Same as above. Width seems to be less of an issue (Probably because the canvas scale to match the width), but it's here just in case. Alternative to Screen.width.
    public float GetCanvasWidth()
    {
        float c;
        c = canvas.sizeDelta.x;
        return c;
    }
}

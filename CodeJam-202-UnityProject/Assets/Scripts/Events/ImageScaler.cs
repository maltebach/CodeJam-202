using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScaler : MonoBehaviour
{
    //public RectTransform reference;

    public RectTransform scaledImage;

    public Vector2 margins;

    public Vector2 scaleFactor;

    //This script is stupid but now its here, its purpose is to make the events image scale properly and it's kinda jank.
    public void Scale()
    {
        scaledImage.sizeDelta = new Vector2(margins.x * scaleFactor.x, margins.y * scaleFactor.y);

    }


    //Used for editor debugging
    /*private void OnDrawGizmos()
    {
        Scale();
        Debug.Log("Scaled image: " + scaledImage.sizeDelta + " - " + scaledImage.rect);
        Debug.Log("Reference image: " + reference.sizeDelta);
    }*/
}

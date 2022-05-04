using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealLikertScale : MonoBehaviour
{
    public Transform likert1;
    public Transform likert2;
    public Transform likert3;
    public Transform likert4;
    public Transform likert5;
    public Transform likert6;
    public Transform likert7;
    public int cursor = -1;
    Vector3 originalSize = new Vector3 (0.65285f, 0.65285f, 0.65285f);
    public Image limg1;
    public Image limg2;
    public Image limg3;
    public Image limg4;
    public Image limg5;
    public Image limg6;
    public Image limg7;
    public Color red;
    private Color originalColor; 

    private void Start() 
    {
        originalColor = limg1.color;
    }
    public void SetLikert(int i)
    {
        ResetScale();
        ResetColor();
        switch(i)
        {
            case 1:
            likert1.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            limg1.color = red;
            break;
            case 2:
            likert2.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            limg2.color = red;
            break;
            case 3:
            likert3.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            limg3.color = red;
            break;
            case 4:
            likert4.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            limg4.color = red;
            break;
            case 5:
            likert5.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            limg5.color = red;
            break;  
            case 6:
            likert6.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            limg6.color = red;
            break;  
            case 7:
            likert7.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            limg7.color = red;
            break;  
            default:

            break;     
        }
        cursor = i;
    
    }
    private void ResetScale()
    {
        likert1.localScale = originalSize;
        likert2.localScale = originalSize;
        likert3.localScale = originalSize;
        likert4.localScale = originalSize;
        likert5.localScale = originalSize;
        likert6.localScale = originalSize;
        likert7.localScale = originalSize;
    }
    private void ResetColor()
    {
        limg1.color = originalColor;
        limg2.color = originalColor;
        limg3.color = originalColor;
        limg4.color = originalColor;
        limg5.color = originalColor;
        limg6.color = originalColor;
        limg7.color = originalColor;
    }
}

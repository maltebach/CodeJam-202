using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetLikert(int i)
    {
        ResetScale();
        switch(i)
        {
            case 1:
            likert1.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            break;
            case 2:
            likert2.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            break;
            case 3:
            likert3.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            break;
            case 4:
            likert4.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            break;
            case 5:
            likert5.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            break;  
            case 6:
            likert6.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            break;  
            case 7:
            likert7.localScale = new Vector3(0.8f, 0.8f, 0.8f);
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
}

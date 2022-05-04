using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileyLikert : MonoBehaviour
{
    public Transform likert1;
    public Transform likert2;
    public Transform likert3;
    public Transform likert4;
    public Transform likert5;

    public int cursor = -1;

    public void SetLikertSmiley(int i)
    {
        ResetScaleSmiley();
        switch(i)
        {
            case 1:
            likert1.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            break;
            case 2:
            likert2.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            break;
            case 3:
            likert3.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            break;
            case 4:
            likert4.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            break;
            case 5:
            likert5.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            break;  
            default:

            break;     
        }
        cursor = i;
    
    }
    private void ResetScaleSmiley()
    {
        likert1.localScale = Vector3.one;
        likert2.localScale = Vector3.one;
        likert3.localScale = Vector3.one;
        likert4.localScale = Vector3.one;
        likert5.localScale = Vector3.one;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<PopUpScript>();
        

    }

    public void Click()
    {
        PopUpScript script = GetComponent<PopUpScript>();
    }
   
}

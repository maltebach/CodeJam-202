using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MoroEventManager.instance.RollEvent();
    }


}

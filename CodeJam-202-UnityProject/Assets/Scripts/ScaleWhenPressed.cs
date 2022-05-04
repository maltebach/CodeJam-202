using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

 [Serializable]
public class ScaleWhenPressed : MonoBehaviour
{
    [SerializeField]
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}

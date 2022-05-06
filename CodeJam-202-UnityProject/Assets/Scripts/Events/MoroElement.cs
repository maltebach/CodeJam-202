using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoroElement : MonoBehaviour
{
    public virtual void CullElement()
    {
        Destroy(this.gameObject);
    }

    public virtual void BuildElement(int index)
    {
        Debug.LogError("BuildElement method must be defined");
    }
}

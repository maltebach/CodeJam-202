using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is just used to ensure classes inheriting this class this are compatible with the moro event handler object.
public abstract class MoroElement : MonoBehaviour
{
    //CullElement is called when the element is destroyed. This is made as a function to support potential extra things needed when removing an element.
    public virtual void CullElement()
    {
        Destroy(this.gameObject);
    }

    //BuildElement is called when the element is "built", or made visible on the screen. It is cheaper to delete and rebuild elements as you scroll than to keep all elements around.
    public virtual void BuildElement()
    {
        Debug.LogError("BuildElement method must be defined");
    }

    //Used by MoroElementHandler to check if the associated element is a likert question.
    public virtual bool IsLikert()
    {
        return false;
    }

    //This method is used by elements to get the relevant data required to build the element in question.
    //e.g. a QuestionBuilder will recieve a specifc MoroQuestion and its associated data such as FFMData object and title.
    public virtual void AttachElement(MoroElementHandler handler)
    {
        Debug.LogError("AttachElement Method must be defined");
    }
}

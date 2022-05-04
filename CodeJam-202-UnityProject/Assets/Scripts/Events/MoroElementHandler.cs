using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoroElementHandler : MonoBehaviour
{
    public GameObject elementPrefab;

    public RectTransform rect;

    public MoroElement element;

    public float height = 1000;

    //public float width = 1000;    //width is currently unused since it's calculated dynamically later on.

    public float xMargin = 150;

    public float disappearBuffer = 150;


    GameObject culledObject;
    bool isCulled = false;

    public int referenceIndex;
    public bool containsEventBuilder = false;
    public bool containsQuestion = false;

    private void Start()
    {
        BuildAssociatedElement();
    }


    private void Update()
    {
        if(ScrollHandler.instance.GetScrollPos() > (0 + height + disappearBuffer - rect.anchoredPosition.y) || ScrollHandler.instance.GetScrollPos() < (-Screen.height - disappearBuffer - rect.anchoredPosition.y))
        {
            if(!isCulled)
            {
                element.CullElement();
                isCulled = true;
            }

        }
        else
        {
            if(isCulled)
            {
                BuildAssociatedElement();
                isCulled = false;
            }
        }
    }

    void BuildAssociatedElement()
    {
        element = Instantiate(elementPrefab, transform.position, Quaternion.identity, rect).GetComponent<MoroElement>();
        if(element == null)
        {
            Debug.LogError("No Element Attached");
        }
        else
        {
            float width = Screen.width - (xMargin * 2);
            element.BuildElement(referenceIndex);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoroElementHandler : MonoBehaviour
{
    public GameObject elementPrefab; //set in inspector. This is the prefab that is built and destroyed by this handler.

    public RectTransform rect; //Set in inspector to this objects rect transform. Could be found in code.

    public float height = 1000; //Height of the element, currently not dynamic and has to match the actual heigh of the element handler prefabs height. Used to move the stacks cursor and for checking if this object is off screen. 

    //public float width = 1000;    //width is currently unused since it's calculated dynamically later on.

    public float xMargin = 150; //Represents the distance from the edge of the screen to the edge of the element.

    public float disappearBuffer = 150; //This just gives a little control as to when an element disappears. Represents a buffer window after element leaves the screen (in pixels)

    public MoroEvent moroEvent;

    public MoroQuestion moroQuestion;

    MoroElement element; //Could be a local variable further down.

    bool isCulled = false; //This bool is used to check whether or not the associated element already exists or not.


    //Variables blow this comment are used only when a likert question is detected in this builders associated element.

    RealLikertScale likert;

    int likertCursor = -1;

    bool likertAnswered = false;

    private void Start()
    {
        BuildAssociatedElement(); //Element Handler is only ever generated when it's supposed to be on screen, so we make sure to build its associated element as soon as the element handler is instantiated.
    }


    private void Update()
    {
        //This if statement checks if the element handler is currently outside of the screen. 
        //If it is outside it will check if the associated element is culled, and if not will cull it.
        //Similarly if the element handler is on screen it will make sure the element is built.
        if(ScrollHandler.instance.GetScrollPos() > (0 + height + disappearBuffer - rect.anchoredPosition.y) 
            || ScrollHandler.instance.GetScrollPos() < (-CanvasRef.instance.GetCanvasHeight() - disappearBuffer - rect.anchoredPosition.y))
        {
            if(!isCulled)
            {
                if (likertCursor == -1 && likert != null && likert.cursor != -1)
                {
                    likertCursor = likert.cursor;
                    if(!likertAnswered)
                    {
                        TestManager.instance.Evaluate(moroQuestion.ffm, likertCursor);
                        MoroEventManager.instance.EvalAllEvents();
                        likertAnswered = true;
                    }

                }

                element.CullElement();
                isCulled = true; //Setting isCulled to true ensures we only try to cull the element once.
            }

        }
        else
        {
            if(isCulled)
            {
                BuildAssociatedElement();
                isCulled = false; //Setting isCulled to false ensures we only try to build the associated element once.
            }
        }
    }

    /// <summary>
    /// This method builds the element associated with the element handler.
    /// This is dependent of two things. The handlers elementPrefab as well as the handlers referenceIndex.
    /// </summary>
    void BuildAssociatedElement()
    {
        element = Instantiate(elementPrefab, transform.position, Quaternion.identity, rect).GetComponent<MoroElement>();
        if(element == null) //Checks if element was attacked to elementPrefab. I think check is largely unneccesary since the line above would cause null references and cause plenty of errors on its own. But this check ensures we only try to modify the element if we get through the check.
        {
            Debug.LogError("No Element Attached");
        }
        else
        {

            element.AttachElement(this);

            float width = CanvasRef.instance.GetCanvasWidth() - (xMargin * 2); //Width is set based on the xMargin value. This is dynamic to support different screen sizes. xMargin is multiplied by 2 because there's a margin on both sides of the element and the element is placed on the middle of the screen.
            element.BuildElement(); //Make sure the element knows its respective reference index so that it can tell manager objects which element it represents.
      
            if(element.IsLikert()) //element.IsLikert() only returns true if the element is a likert question.
            {
                likert = element.GetComponent<RealLikertScale>();
                
                //if(likertCursor != -1)
                {
                likert.SetLikert(likertCursor);
                }
            }
        }
    }

}

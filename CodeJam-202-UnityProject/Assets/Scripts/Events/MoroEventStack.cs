using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoroEventStack : MonoBehaviour
{
    [Header("Element Generation")]
    //public List<MoroElementHandler> elements = new List<MoroElementHandler>(); //List of elements used to keep track of the order of elements generated. CURRENTLY UNUSED.

    [Range(0f,1f)]
    public float questionFrequency = 0.1f; //This value represents the chance a question is generated instead of an event.


    [Header("Formatting")]
    public float gap = 100; //This value represents the gap between elements on the screen in pixels.
    public float startYOffset = 0; //This value is used for formatting. This will offset the entire list of elements. Used to position the first element correctly.

    [Header("Debug")]
    public int debugStackCount; //Debug value used for testing. Can also be used to ensure a certain number of elements are generated on startup.

    //private variables, used internally in this script.
    float cursor = 0; //This number represents the position of the next element generated. It's also used to check if a new element should be generated when scrolling.

    bool lastWasQuestion = false; //This is used to prevent several questions from being generated in a row.

    private void Start()
    {
        MoveCursor(startYOffset); //Makes sure the cursor is offset such that the first event fully generates on screen.
        DebugPopulateStack(debugStackCount); //Used only if a certain amount of elements has to be present on startup.
    }

    /// <summary>
    /// This just loops through a number of times to generate that amount of elements on startup. Used primarily for debug purposes.
    /// </summary>
    /// <param name="j"></param>
    void DebugPopulateStack(int j)
    {
        for (int i = 0; i < j; i++)
        {
            ExpandStack();
        }

    }

    /// <summary>
    /// Used to add an element to the "stack". This method gets an element from the event or question manager objects and generates the element at the appropriate place.
    /// </summary>
    void ExpandStack()
    {
        MoroElementHandler element; //Create reference to an element handler. Currently empty.

        if(!lastWasQuestion) //Check to see if last element was a question. This is done to avoid having multiple questions in a row.
        {
            float rand = Random.Range(0f, 1f); //Generate a float from 0 to 1. If this number is smaller than the questionFrequency variable, we generate a question instead of an event.
            if (rand < questionFrequency && !MoroQuestionManager.instance.OutOfQuestions())
            {
                //Define our element handler as one for a specific question.
                element = MoroQuestionManager.instance.GetQuestion(MoroQuestionManager.instance.GetNextQuestion(), GetCursor()); 

                //Since we just generated a question we set lastWasQuestion to true, such that we do not generate another question right after.
                lastWasQuestion = true;
            }
            else
            {
                if (!MoroEventManager.instance.OutOfEvents())
                {
                    element = MoroEventManager.instance.GetBuilder(MoroEventManager.instance.GetNextEvent(), GetCursor()); //Define our element handler as one for a specific event.
                }
                else
                {
                    ScrollHandler.instance.StopScroll();
                    return;
                }
            }
        }
        else
        {
            if (!MoroEventManager.instance.OutOfEvents())
            {
                element = MoroEventManager.instance.GetBuilder(MoroEventManager.instance.GetNextEvent(), GetCursor()); //Define our element handler as one for a specific event.
                lastWasQuestion = false; //Set to false as we would like to be able to generate questions again.
            }
            else
            {
                ScrollHandler.instance.StopScroll();
                return;
            }

        }
        MoveCursor(element.height + gap); //We move the cursor the distance of our elements height and the gap we want between elements. This supports elements of differing heights.
    }
    
    /// <summary>
    /// Method used to move the cursor.
    /// </summary>
    /// <param name="amount"></param>
    void MoveCursor(float amount)
    {
        cursor -= amount; //Is turned negative as the cursor moves down the page. This is done here such that all values we use to determine how far to move the cursor can be positive.
    }

    /// <summary>
    /// This takes the cursor float value and turns it into a vector3 as it represents a point inside the scrollable area.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCursor()
    {
        Vector3 cursorPos = Vector3.zero; //The cursor is always in the middle of the screen, which is represented by x = 0, and we only ever move the y value as it represents how far down on the page the cursor is.
        cursorPos.y = cursor;
        return cursorPos;
    }

    /// <summary>
    /// This method checks whether or not the cursor has been scrolled off screen and a new element should be generated.
    /// </summary>
    /// <returns></returns>
    bool CheckOutOfBounds()
    {
        //Honestly I don't remember why this works. 
        //As far as I can tell, we check if cursor is below the screen in relation to an offset based on how far the scrollview is scrolled down.
        if ((-cursor) < ScrollHandler.instance.GetScrollPos() + CanvasRef.instance.GetCanvasHeight()) 
        {
            return true;
        }
        return false;
    }

    //We check for out of bounds, and if we hit one we expand the stack.
    private void Update()
    {
        if(CheckOutOfBounds())
        {
            ExpandStack();
        }
    }
}

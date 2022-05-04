using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoroEventStack : MonoBehaviour
{

    public List<MoroElementHandler> elements = new List<MoroElementHandler>();

    /// <summary>
    /// Chance of generating a question rather than an event.
    /// </summary>
    [Range(0f,1f)]
    public float questionFrequency = 0.1f;

    public int debugStackCount;
    public float gap = 100;
    public float startYOffset = 0;

    float cursor = 0;
    Vector3 cursorVector = Vector3.zero;
    private void Start()
    {
        cursor = -startYOffset;
        DebugPopulateStack(debugStackCount);
    }

    public void AddElement(MoroElementHandler element)
    {
        elements.Add(element);
    }

    public void DebugPopulateStack(int j)
    {
        for (int i = 0; i < j; i++)
        {
            ExpandStack();
        }

    }

    public void ExpandStack()
    {
        MoroElementHandler element;
        float rand = Random.Range(0f,1f);
        if(rand < questionFrequency)
        {
            element = QuestionManager.instance.GetQuestion(TestManager.instance.GetFilteredIndex());
        } 
        else
        {
            element = MoroEventManager.instance.GetBuilder(TestManager.instance.GetFilteredIndex());
        }
        AddElement(element);
        MoveCursor(element.height + gap);
        Debug.Log("Cursor pos: " + GetCursor());
        Debug.Log("Scroll pos: " + ScrollHandler.instance.GetScrollPos());
    }

    public void MoveCursor(float amount)
    {
        cursor -= amount;
    }

    public Vector3 GetCursor()
    {
        Vector3 cursorPos = cursorVector;
        cursorPos.y = cursor;
        return cursorPos;
    }

    bool CheckOutOfBounds()
    {
        //Debug.Log("Cursor pos: " + cursor);
        //Debug.Log("Scroll pos: " + (ScrollHandler.instance.GetScrollPos()));
        if((-cursor) < ScrollHandler.instance.GetScrollPos() + Screen.height)
        {
            Debug.Log("Out of bounds triggered");
            return true;
        }
        return false;
    }

    private void FixedUpdate()
    {
        if(CheckOutOfBounds())
        {
            ExpandStack();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MoroEvent is a class that just contains data relevent to any given event.
/// Things such as the name of the event, name of the venue, price etc.
/// </summary>
[System.Serializable]
public class MoroEvent
{
    public string eventName;
    public string eventDescription;
    public float price;
    public float distance;
    public string venue;
    public string address;

    public Sprite eventImage;
    public FFMData ffm;
    public Date date;

    public float relatabilityValue;



}

/// <summary>
/// This class is also just a data class used primarily to make the code look nicer.
/// </summary>
[System.Serializable]
public class Date
{
    public int day;
    public int month;
    public int year;
    public int startTime;
    public int endTime;
}

public class MoroEventOrderer : IComparer<MoroEvent>
{
    public int Compare(MoroEvent x, MoroEvent y)
    {
        int compareEvent = x.relatabilityValue.CompareTo(y.relatabilityValue);
        return compareEvent;
    }
}

/// <summary>
/// This singleton contains a list of possible events and can provide a builder element to build any specific element based on an index value.
/// </summary>
public class MoroEventManager : MonoBehaviour
{
    //Singleton reference
    public static MoroEventManager instance;

    [Header("List of Events")]
    public List<MoroEvent> moroEvents = new List<MoroEvent>(); //This list contains all events possible. This list is not sorted or filtered.
    public List<MoroEvent> unshownEvents = new List<MoroEvent>();

    [Header("References")]
    public GameObject eventPrefab; //The prefab that is instantiated and populated by the MoroEventBuilder script. Event prefab must include a MoroEventBuilder script.

    [Header("ScrollView")]
    Transform parentTransform; //This transform should be the "content" Game Object of the Scrollview in the scene. Used to set generated events as child.


    bool isDone = false;

    //singleton logic; makes sure only one MoroEventManager exists.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        foreach (MoroEvent item in moroEvents)
        {
            unshownEvents.Add(item);
        }
    }

    /// <summary>
    /// This method will generate an instance of an element handler object based on an input index value that corrisponds to a specifc event in this objects event list.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public MoroElementHandler GetBuilder(MoroEvent moro, Vector3 cursor)
    {
        //Instantiate an element handler object based on a prefab. The object is instantiated at the end of the feed (Or the position of the EventStack's cursor)
        MoroElementHandler element = Instantiate(eventPrefab, cursor, Quaternion.identity).GetComponent<MoroElementHandler>();

        //ReferenceIndex is a number used by the element so it knows which event it represents. TODO CHANGE COMMENT
        element.moroEvent = moro;

        //Setting the transform like this instead of in the Instantiate method, because we need the world position functionality of this method for it to be placed properly
        element.transform.SetParent(parentTransform, false);
        return element;
    }

    public void SetParentTransform(Transform pt)
    {
        parentTransform = pt;
    }

    float EvaluateEvent(FFMData ffm)
    {
        float relatability = 0;


        float o = 0;
        float a = 0;
        float e = 0;
        float c = 0;
        float n = 0;

        float numOfFactors = 0;

        if (ffm.openness != 0)
        {
            o = TestManager.instance.openness - ffm.openness;
            o = Mathf.Abs(o);
            numOfFactors++;
        }

        if (ffm.agreeableness != 0)
        {
            a = TestManager.instance.agreeableness - ffm.agreeableness;
            a = Mathf.Abs(a);
            numOfFactors++;
        }

        if (ffm.extraversion != 0)
        {
            e = TestManager.instance.extraversion - ffm.extraversion;
            e = Mathf.Abs(e);
            numOfFactors++;
        }

        if (ffm.conscientiousness != 0)
        {
            c = TestManager.instance.conscientiousness - ffm.conscientiousness;
            c = Mathf.Abs(c);
            numOfFactors++;
        }

        if (ffm.neuroticism != 0)
        {
            n = TestManager.instance.neuroticism - ffm.neuroticism;
            n = Mathf.Abs(n);
            numOfFactors++;
        }

        if (numOfFactors != 0)
            relatability = (o + a + e + c + n) / numOfFactors;
        else relatability = 0;

        return relatability;
    }

    public void EvalAllEvents()
    {
        foreach (MoroEvent item in unshownEvents)
        {
            item.relatabilityValue = EvaluateEvent(item.ffm);
        }

        IComparer<MoroEvent> comparer = new MoroEventOrderer();
        unshownEvents.Sort(comparer);
    }

    private void Start()
    {
        EvalAllEvents();
    }

    public MoroEvent GetNextEvent()
    {
        MoroEvent moro;

        moro = unshownEvents[0];

        unshownEvents.RemoveAt(0);

        if (unshownEvents.Count == 0)
        {
            isDone = true;
        }
        return moro;
    }

    public bool OutOfEvents()
    {
        return isDone;
    }
} 
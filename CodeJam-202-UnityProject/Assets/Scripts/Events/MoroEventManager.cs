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
/// <summary>
///This is used to make events comparable, and sortable as a result of that, by the relatability value.
/// </summary>
public class MoroEventOrderer : IComparer<MoroEvent>
{
    //I believe if x is smaller than y this returns a negative number, 0 if equal, and positive if x is bigger. It's used by the built in sorting method on list.
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

    [Header("Debug")]
    public bool randomMode = false; //If set to true. We will return a randomised list and will not sort. This is used for user testing purposes.
    System.Random rnd = new System.Random(); //The shuffle method we use, requres a System.Random object. (Not a UnityEngine.Random object)

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
    /// This method will generate an instance of an element handler object based on an input MoroEvent.
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

    /// <summary>
    /// This is used by the scrollview to properly set the parent transform, this is done this way because the scrollview and this class are supposed to exist in two different scenes.
    /// </summary>
    /// <param name="pt"></param>
    public void SetParentTransform(Transform pt)
    {
        parentTransform = pt;
    }

    /// <summary>
    /// This is a method that collapses the 5 ffm traits into a single number that represents how close this events assigned ffm values are to the users test scores.
    /// We use this single number to sort the list of events from most to least relevant.
    /// </summary>
    /// <param name="ffm"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    float EvaluateEvent(FFMData ffm, int index)
    {
        float relatability = 0;

        //One float for each ffm trait.
        float o = 0;
        float a = 0;
        float e = 0;
        float c = 0;
        float n = 0;

        //Number of factors is important, as we do not wish to include ffm traits that do not have any bearing on the results into these calculations.
        //Therefor we keep track of how many factors the event in question is affected by so we can take the average at the end.
        float numOfFactors = 0;

        //This bit of math is done for each of the 5 traits. 
        //We take the difference between the user test scores and the event's scores.
        if (ffm.openness != 0)      //If a trait is 0, it means the factor has no correlation with the event.
        {
            //Taking the difference gives us a number that tells us how far the event's trait score is from the user test score. A lower number here means they are closer together and therefor more relevant for the user.
            o = TestManager.instance.openness - ffm.openness;

            //We take the absolute value, as we don't want to have negative numbers in this calculation.
            o = Mathf.Abs(o);

            //We count this factor (If this trait is not 0)
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

        //We need to check if number of factors is 0, this would likely mean a mistake on the person who added the event's part.
        if (numOfFactors != 0)
        {
            //We take the average of all of the previous calculations to end up with our single number that represents "Relatability". In reality this number just shows how close the user's test scores are to the event's scores.
            relatability = (o + a + e + c + n) / numOfFactors;
        }
        else
        {
            //If an event has 0 factors we print a warning to the console to alert the developer.
            Debug.LogWarning("Event has 0 factors! Index: " + index);

            //We also set the relatability to 50. This number could be anything but 50 technically represents the absolute average.
            //We do this, as otherwise it would return a relatability score of 0, also meaning it would appear first in the list always. In case an error like this makes it to production we don't want to place a random event first.
            relatability = 50;
        }

        return relatability;
    }

    /// <summary>
    /// This loops through all unshown events and calculates their relatability value using the method above. Then it sorts the list putting lower values (more relevant events) first.
    /// </summary>
    public void EvalAllEvents()
    {
        if(!randomMode) //If random mode is enabled we will not evaluate and sort events.
        {
            for (int i = 0; i < unshownEvents.Count; i++)
            {
                unshownEvents[i].relatabilityValue = EvaluateEvent(unshownEvents[i].ffm, i);
            }

            //Sorting things a bit complicated to understand how they work since I don't know the inner workings of List.sort(), but it uses an extension class defined above this class that allows us to compare relatability values on events.
            IComparer<MoroEvent> comparer = new MoroEventOrderer();
            unshownEvents.Sort(comparer);
        }
    }

    //We start evaluating all events to the default user profile.
    private void Start()
    {
        EvalAllEvents();

        if(randomMode) //If random mode is enabled we will just shuffle the events.
        {
            unshownEvents.Shuffle(rnd);
        }
    }

    /// <summary>
    /// This method is used get the first moro event in the sorted list unshownEvents, the first event is the event with the lowest (and therefor most relevant) relatability value.
    /// </summary>
    /// <returns></returns>
    public MoroEvent GetNextEvent()
    {
        MoroEvent moro;

        moro = unshownEvents[0];

        unshownEvents.RemoveAt(0);

        //If the list is empty we set a bool to true that can later tell the system to stop trying to generate more events.
        if (unshownEvents.Count == 0)
        {
            isDone = true;
        }
        return moro;
    }

    /// <summary>
    /// This is used to check if there are no more events left to generate. If true is recieved the MoroEventStack will stop trying to generate more events.
    /// </summary>
    /// <returns></returns>
    public bool OutOfEvents()
    {
        return isDone;
    }
} 
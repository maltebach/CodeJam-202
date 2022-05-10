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
/// This singleton contains a list of possible events and can provide a builder element to build any specific element based on an index value.
/// </summary>
public class MoroEventManager : MonoBehaviour
{
    //Singleton reference
    public static MoroEventManager instance;

    [Header("List of Events")]
    public List<MoroEvent> moroEvents = new List<MoroEvent>(); //This list contains all events possible. This list is not sorted or filtered.

    [Header("References")]
    public GameObject eventPrefab; //The prefab that is instantiated and populated by the MoroEventBuilder script. Event prefab must include a MoroEventBuilder script.

    [Header("ScrollView")]
    Transform parentTransform; //This transform should be the "content" Game Object of the Scrollview in the scene. Used to set generated events as child.

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
    }

    /// <summary>
    /// This method will generate an instance of an element handler object based on an input index value that corrisponds to a specifc event in this objects event list.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public MoroElementHandler GetBuilder(int i, Vector3 cursor)
    {
        //Instantiate an element handler object based on a prefab. The object is instantiated at the end of the feed (Or the position of the EventStack's cursor)
        MoroElementHandler element = Instantiate(eventPrefab, cursor, Quaternion.identity).GetComponent<MoroElementHandler>();

        //ReferenceIndex is a number used by the element so it knows which event it represents.
        element.referenceIndex = i;

        //Setting the transform like this instead of in the Instantiate method, because we need the world position functionality of this method for it to be placed properly
        element.transform.SetParent(parentTransform, false);
        return element;
    }

    public void SetParentTransform(Transform pt)
    {
        parentTransform = pt;
    }
} 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Date
{
    public int day;
    public int month;
    public int year;
    public int startTime;
    public int endTime;
}

[System.Serializable]
public class MoroEvent
{
    public string eventName;
    public string eventDescription;
    public float price;
    public float distance;
    public Date date;
    public string venue;
    public string address;
    public Sprite eventImage;
    public float openness = 0;
    public float conscientiousness = 0;
    public float extraversion = 0;
    public float agreeableness = 0;
    public float neuroticism = 0;
}

public class MoroEventManager : MonoBehaviour
{
    public static MoroEventManager instance;

    [Header("List of Events")]
    public List<MoroEvent> moroEvents = new List<MoroEvent>();

    [Header("References")]

    public GameObject eventPrefab; //The prefab that is instantiated and populated by the MoroEventBuilder script. Event prefab must include a MoroEventBuilder script.

    public MoroEventStack stack;

    [Header("ScrollView")]
    public Transform parentTransform;

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

    public MoroElementHandler GetBuilder(int i)
    {
        MoroElementHandler element = Instantiate(eventPrefab, stack.GetCursor(), Quaternion.identity).GetComponent<MoroElementHandler>();
        element.referenceIndex = i;
        element.transform.SetParent(parentTransform, false);
        return element;
    }
} 
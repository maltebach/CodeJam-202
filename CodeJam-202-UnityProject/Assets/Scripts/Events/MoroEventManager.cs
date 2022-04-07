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
}

public class MoroEventManager : MonoBehaviour
{
    public static MoroEventManager instance;

    [Header("List of Events")]
    public List<MoroEvent> moroEvents = new List<MoroEvent>();

    [Header("References")]
    //Currently there is not reason to have multiple eventbuilders, but it could be useful for future use.
    public List<MoroEventBuilder> moroEventBuilders = new List<MoroEventBuilder>();

    public GameObject eventPrefab;

    public Canvas canvas;


    [Header("Layout")]
    //These numbers determine the size and placement of the event on screen.
    //xMargin is the horizontal space between the edge of the screen and the event (on both sides), yMargin is the same but vertial.
    //x- and yOffset adds an offset in pixels to the event on screen.
    public float xMargin = 50;

    public float yMargin = 250;

    public float xOffset = 0;

    public float yOffset = 0;

    //singleton logic; makes sure only one MoroEventManager exists.
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(this);
        }
    }

    //Takes an int as an input to build the specific event at that index in the event list.
    public void BuildEvent(int index)
    {
        //Calculating the middle of the screen to figure out where to place the event after its built
        //x- and yOffset values are added here to move the spot where the event is placed.
        Vector3 middleOfScreen = new Vector3(Screen.width / 2 + xOffset, Screen.height / 2 + yOffset, 0);

        //Instansiate the event prefab and get the MoroEventBuilder script associated with that prefab.
        MoroEventBuilder mrb = Instantiate(eventPrefab, middleOfScreen, Quaternion.identity, canvas.transform).GetComponent<MoroEventBuilder>();

        //Check if MoroEventBuilder was found on the prefab.
        if(mrb == null)
        {
            //Notifies the user in the editor that the prefab is missing the MoroEventBuilder component.
            Debug.LogError("MoroEventBuilder missing from event prefab!");
        }
        else
        {
            //Calculating width and height of event prefab based on the users screen size and a desired margin. Margin values are doubled as they represent the margin on only one side of the event.
            float width = Screen.width - (xMargin * 2);
            float height = Screen.height - (yMargin * 2);

            mrb.BuildEvent(moroEvents[index], width, height, canvas);
        }
        //Adding the eventBuilder to a list to represent the active event.
        moroEventBuilders.Add(mrb);
    }

    //Generates a random int between 0 and the amount of events in the event list.
    public int GetRandomEventIndex()
    {
        int randomIndex = Random.Range(0, moroEvents.Count);
        Debug.Log("Random Index: " + randomIndex);
        return randomIndex;
    }
    
    //Removes an active event prefab
    public void RemoveEvent(int builderIndex)
    {
        MoroEventBuilder mrb = moroEventBuilders[builderIndex];
        moroEventBuilders.RemoveAt(builderIndex);
        mrb.DeleteEvent();
    }

    //Logic used to remove any existing events and generating a new one based on a random index from GetRandomEventIndex()
    public void RollEvent()
    {
        if (moroEventBuilders.Count != 0)
        {
            RemoveEvent(0);
        }
        BuildEvent(GetRandomEventIndex());

    }
}
 
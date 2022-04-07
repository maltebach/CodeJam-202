using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoroEventBuilder : MonoBehaviour
{
    public TMP_Text eventName;

    public TMP_Text eventDescription;

    public TMP_Text eventDistance;

    public TMP_Text eventPrice;

    public TMP_Text eventVentue;

    public TMP_Text eventAddress;

    public TMP_Text eventDate;

    public TMP_Text eventTime;

    public Image background;

    public Image eventImage;

    RectTransform rt;

    public MoroEvent moroEvent;

    public Color32 textColor;

    public Color32 bgColor;
    private void Awake()
    {
        if (rt == null)
            rt = GetComponent<RectTransform>();
    }

    public void BuildEvent(MoroEvent moro, float width, float height, Canvas c)
    {
        moroEvent = moro;
        UpdateEvent(width, height, c);

    }

    public void UpdateEvent(float width, float height, Canvas c)
    {
        rt.sizeDelta = new Vector2(width, height);


        //Set all attributes from given event.
        eventName.text = moroEvent.eventName;

        eventDescription.text = moroEvent.eventDescription;

        eventPrice.text = moroEvent.price.ToString() + " DKK";

        eventVentue.text = moroEvent.venue;

        eventAddress.text = moroEvent.address;

        //Hvis distance i meter er over 1000, konverteres det til kilometer
        if (moroEvent.distance > 1000)
        {
            float dist = moroEvent.distance / 1000;
            eventDistance.text = dist.ToString() + " km";
        }
        else
        {
            eventDistance.text = moroEvent.distance.ToString() + " m";
        }

        //Date formatting, a lot of ugly adding together of strings.
        string yearShort = "NULL";
        if(moroEvent.date.year.ToString().Length == 4)
        {
            yearShort = moroEvent.date.year.ToString()[2].ToString() + moroEvent.date.year.ToString()[3].ToString();
        }
        else if(moroEvent.date.year.ToString().Length == 2)
        {
            yearShort = moroEvent.date.year.ToString();
        }
        else
        {
            Debug.LogError("Unrecognised year format: " + moroEvent.date.year + " Please use either 2 or 4 characters (ie: 2022 or 22)");
        }

        string date = moroEvent.date.day.ToString() + "/" + moroEvent.date.month.ToString() + "/" + yearShort;
        eventDate.text = date;

        //Time Formatting, see comment above.
        string time1 = "NULL";
        string time2 = "NULL";

        if(moroEvent.date.startTime.ToString().Length == 4)
        {
            time1 = moroEvent.date.startTime.ToString()[0].ToString() + moroEvent.date.startTime.ToString()[1].ToString() + ":" + moroEvent.date.startTime.ToString()[2].ToString() + moroEvent.date.startTime.ToString()[3].ToString();
        }
        else if (moroEvent.date.startTime.ToString().Length == 2)
        {
            time1 = moroEvent.date.startTime.ToString() + ":00";
        } 
        else
        {
            Debug.LogError("Unrecognised time format: " + moroEvent.date.startTime + " Please use either 2 or 4 characters (ie: 1430 or 14)");
        }

        if (moroEvent.date.endTime.ToString().Length == 4)
        {
            time2 = moroEvent.date.endTime.ToString()[0].ToString() + moroEvent.date.endTime.ToString()[1].ToString() + ":" + moroEvent.date.endTime.ToString()[2].ToString() + moroEvent.date.endTime.ToString()[3].ToString();
        }
        else if (moroEvent.date.endTime.ToString().Length == 2)
        {
            time2 = moroEvent.date.endTime.ToString() + ":00";
        }
        else
        {
            Debug.LogError("Unrecognised time format: " + moroEvent.date.endTime + " Please use either 2 or 4 characters (ie: 1430 or 14)");
        }

        eventTime.text = time1 + " - " + time2;

        //set Image
        eventImage.sprite = moroEvent.eventImage;
    }

    public void DeleteEvent()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// This script handles populating an event prefab with data from the MoroEventManager component. 
/// This class is required to exist on event prefabs.
/// </summary>
public class MoroEventBuilder : MonoBehaviour
{

    //All of these TMP_Text and image attributes represents a text field on the event prefab which this script aims to populate with data.
    public TMP_Text eventName;

    public TMP_Text eventDescription;

    public TMP_Text eventDistance;

    public TMP_Text eventPrice;

    public TMP_Text eventVentue;

    public TMP_Text eventAddress;

    public TMP_Text eventDate;

    public TMP_Text eventTime;

    //public Image background;

    public Image eventImage;

    RectTransform rt; //rt is used to set the size of the event prefab.

    public MoroEvent moroEvent; //used primarily just to make the code look a bit cleaner.

    //public Color32 textColor; //Unused color variables

    //public Color32 bgColor;

    //ensures rt is set during awake
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
        rt.sizeDelta = new Vector2(width, height); //Sets the size of the event on the screen.


        //Set all attributes from given event.
        eventName.text = moroEvent.eventName;

        eventDescription.text = moroEvent.eventDescription;

        eventVentue.text = moroEvent.venue;

        eventAddress.text = moroEvent.address;

        //This if / else statement checks if the price is 0, if so it will write "Gratis!" instead of the price.
        if (moroEvent.price == 0)
        {
            eventPrice.text = "Gratis!";
        } 
        else
        {
            eventPrice.text = moroEvent.price.ToString() + " DKK"; //Adding "DKK" at the end as that is the correct currency.
        }

        //This if / else statement takes the distance in meters and konverts it to km if the value is greater than 1000 meters.
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
        string yearShort = "NULL"; //yearShort is used to remove the first 2 digits of the year as the formatting only uses the last 2 digits.

        //This if / else statement checks to see if the year value is formatted as either 2 or 4 digits. If 4 digits it will remove the first 2, if 2 it uses value as is. If any other length it will default to NULL and notify the user of a formatting error.
        if (moroEvent.date.year.ToString().Length == 4)
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

        string dayLong = ""; //daylong is used to place a 0 in front of single digit dates.7

        //This if / else statement checks to see if the day value is a single digit, if it is it will place a 0 in front, if not it will just use the value as is.
        if (moroEvent.date.day.ToString().Length == 1)
        {
            dayLong = "0" + moroEvent.date.day.ToString();
        } 
        else
        {
            dayLong = moroEvent.date.day.ToString();
        }

        string monthLong = ""; //monthLong is used to place a 0 in front of single digit months.

        //This if / else statement checks to see if the month value is a single digit, if it is it will place a 0 in front, if not it will just use the value as is.
        if (moroEvent.date.month.ToString().Length == 1)
        {
            monthLong = "0" + moroEvent.date.month.ToString();
        }
        else
        {
            monthLong = moroEvent.date.month.ToString();
        }

        //Finally the dayLong, monthLong and yearShort values are put together into the correct date formatting (DD/MM/YY)
        string date = dayLong + "/" + monthLong + "/" + yearShort;
        eventDate.text = date;

        //Time Formatting, see comment above.
        string time1 = "NULL";
        string time2 = "NULL";

        //This ugly conditional statement is all to ensure that the startTime value is formatted properly. It will default to NULL and notify the user if incorrect formatting is used.
        if(moroEvent.date.startTime.ToString().Length == 4)
        {
            time1 = moroEvent.date.startTime.ToString()[0].ToString() + moroEvent.date.startTime.ToString()[1].ToString() + ":" + moroEvent.date.startTime.ToString()[2].ToString() + moroEvent.date.startTime.ToString()[3].ToString();
        }
        else if (moroEvent.date.startTime.ToString().Length == 2)
        {
            time1 = moroEvent.date.startTime.ToString() + ":00";
        } 
        else if (moroEvent.date.startTime == 0)
        {
            time1 = "00:00";
        }
        else if (moroEvent.date.startTime.ToString().Length == 3)
        {
            time1 = "0" + moroEvent.date.startTime.ToString()[0].ToString() + ":" + moroEvent.date.startTime.ToString()[1].ToString() + moroEvent.date.startTime.ToString()[2].ToString();
        }
        else
        {
            Debug.LogError("Unrecognised time format: " + moroEvent.date.startTime + " Please use either 2, 3 or 4 characters (ie: 1430 (14:30), 14 (14:00) or 200 (02:00))");
        }

        //This ugly conditional statement is all to ensure that the endTime value is formatted properly. It will default to NULL and notify the user if incorrect formatting is used.
        if (moroEvent.date.endTime.ToString().Length == 4)
        {
            time2 = moroEvent.date.endTime.ToString()[0].ToString() + moroEvent.date.endTime.ToString()[1].ToString() + ":" + moroEvent.date.endTime.ToString()[2].ToString() + moroEvent.date.endTime.ToString()[3].ToString();
        }
        else if (moroEvent.date.endTime.ToString().Length == 2)
        {
            time2 = moroEvent.date.endTime.ToString() + ":00";
        }
        else if (moroEvent.date.endTime == 0)
        {
            time2 = "00:00";
        }
        else if (moroEvent.date.endTime.ToString().Length == 3)
        {
            time2 = "0" + moroEvent.date.endTime.ToString()[0].ToString() + ":" + moroEvent.date.endTime.ToString()[1].ToString() + moroEvent.date.endTime.ToString()[2].ToString();
        }
        else 
        {
            Debug.LogError("Unrecognised time format: " + moroEvent.date.endTime + " Please use either 2, 3 or 4 characters (ie: 1430 (14:30), 14 (14:00) or 200 (02:00))");
        }

        //The startTime and endTime values are formatted correctly (ie: 18:00 - 02:00)
        eventTime.text = time1 + " - " + time2;

        //set Image
        eventImage.sprite = moroEvent.eventImage;
    }

    //Called by MoroEventManager
    public void DeleteEvent()
    {
        Destroy(this.gameObject);
    }
}

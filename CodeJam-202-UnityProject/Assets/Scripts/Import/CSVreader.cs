using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class EventList
{
    public MoroEvent[] Event;

}
public class CSVreader : MonoBehaviour

//https://www.youtube.com/watch?v=tI9NEm02EuE 

{
    public TextAsset textAssetData;

    //public EventList MoroEventList = new EventList();

    public List<MoroEvent> moroEvents = new List<MoroEvent>();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int collumns = 14; //Det ville være fedt hvis programmet selv kunne finde ud af hvor mange collumns den skal springe over, så den ville være dynamisk istedet for statisk.
        int tableSize = (data.Length / collumns) - 1;

        
        for (int i = 0; i < tableSize; i++)
        {
            //Debug.Log(int.Parse(data[(i + 1) * collumns]));
            MoroEvent moro = new MoroEvent();
            Date date = new Date();
            moro.date = date;
            FFMData ffm = new FFMData();
            moro.ffm = ffm;
            int j = 0; 


            moro.date.day = int.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.date.month = int.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.date.year = int.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.date.startTime = int.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.date.endTime = int.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.eventName = data[(i + 1) * collumns + j];
            j++;

            moro.price = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.venue = data[(i + 1) * collumns + j];
            j++;

            moro.address = data[(i + 1) * collumns + j];
            j++;

            moro.ffm.openness = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.ffm.conscientiousness = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.ffm.extraversion = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.ffm.agreeableness = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moro.ffm.neuroticism = float.Parse(data[(i + 1) * collumns + j]);
            j++;

            moroEvents.Add(moro);


        }

    }
}





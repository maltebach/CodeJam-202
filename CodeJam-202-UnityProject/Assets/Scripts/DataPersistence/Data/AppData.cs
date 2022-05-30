// This script is based off of Trevor Mocks Youtube video: https://www.youtube.com/watch?v=aUi9aijvpgs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AppData
{
    public float openness;
    public float conscientiousness;
    public float extraversion;
    public float agreeableness;
    public float neuroticism;

    public Dictionary<string, bool> eventsSaved;

    //The values created in this constructor will be the default values
    // the game starts with when there's no data to load
    public AppData()
    {

        eventsSaved = new Dictionary<string, bool>();
    }
}

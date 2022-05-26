// This script is based off of Trevor Mocks Youtube video: https://www.youtube.com/watch?v=aUi9aijvpgs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int deathCount;

    public Dictionary<string, bool> eventsSaved;

    public GameData()
    {
        this.deathCount = 0;
        
        eventsSaved = new Dictionary<string, bool>();
    }
}

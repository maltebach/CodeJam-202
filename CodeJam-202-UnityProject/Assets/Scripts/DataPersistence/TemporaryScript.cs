// This script is based off of Trevor Mocks Youtube video: https://www.youtube.com/watch?v=aUi9aijvpgs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryScript : MonoBehaviour, IDataPersistence
{

   public int deathCount = 1;


    public void LoadData (GameData data)
    {
        this.deathCount = data.deathCount;
    }

    public void SaveData (ref GameData data)
        //Actually, we do not need the 'ref' keyword in C# as non-primitive types are automatically. 
    {
        data.deathCount = this.deathCount;
    }
}

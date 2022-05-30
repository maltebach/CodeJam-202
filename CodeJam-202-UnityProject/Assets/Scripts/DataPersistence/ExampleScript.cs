// This script is based off of Trevor Mocks Youtube video: https://www.youtube.com/watch?v=aUi9aijvpgs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ExampleScript now inherits from IDataPersistence
public class ExampleScript : MonoBehaviour, IDataPersistence
{

   public int openness = 1;


    public void LoadData (AppData data)
    {
        this.openness = data.openness;
    }

    public void SaveData (ref AppData data)
        //Actually, we do not need the 'ref' keyword in C# as non-primitive types
        //are automatically passed by reference. 
    {
        data.openness = this.openness;
    }
}

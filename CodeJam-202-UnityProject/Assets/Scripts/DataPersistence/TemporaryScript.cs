// This script is based off of Trevor Mocks Youtube video: https://www.youtube.com/watch?v=aUi9aijvpgs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TemporaryScript now inherits from IDataPersistence
public class TemporaryScript : MonoBehaviour, IDataPersistence
{

   public int fiveFactorModelScore = 1;


    public void LoadData (AppData data)
    {
        this.fiveFactorModelScore = data.fiveFactorModelScore;
    }

    public void SaveData (ref AppData data)
        //Actually, we do not need the 'ref' keyword in C# as non-primitive types
        //are automatically passed by reference. 
    {
        data.fiveFactorModelScore = this.fiveFactorModelScore;
    }
}

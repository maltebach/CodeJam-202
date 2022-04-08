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
    {
        data.deathCount = this.deathCount;
    }
}

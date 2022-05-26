// This script is based off of Trevor Mocks Youtube video: https://www.youtube.com/watch?v=aUi9aijvpgs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
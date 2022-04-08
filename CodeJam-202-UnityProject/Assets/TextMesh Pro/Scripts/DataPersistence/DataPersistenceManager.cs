using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq giver syntax muligheder for at finde IDataPersistence Objekter
using System.Linq;

/// <summary>
/// 
/// </summary>

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }
        instance = this;
    }


    //Loads the game when started
    private void Start()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // - Load any saved data from a file using the data handler
        //if no data can be loaded, initialize to a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        // - push the Loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("Loaded death count = " + gameData.deathCount);
    }


    public void SaveGame()
    {
        // - pass the data to other scripts so they can update it
        // (again, we are passing by 'ref' (reference) because we want to modify the value, and not only read it.
        // if we had passed without 'ref' (passing by value) it would copy the data instead).

        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
             dataPersistenceObj.SaveData(ref gameData);
        }

        Debug.Log("Saved death count = " + gameData.deathCount);

        // TODO - save that data to a file using the data handler
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    /// <summary>
    /// Because we are using System.Linq, we can find all objects/scripts, which use/implement IDataPersistence in our scene.
    /// </summary>
    /// <returns></returns>
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        //Returnere/constructer en ny liste med alle objector der implementerer interface.
        return new List<IDataPersistence>(dataPersistenceObjects);

    }
}

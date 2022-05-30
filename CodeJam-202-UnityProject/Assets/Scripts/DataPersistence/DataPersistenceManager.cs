// This script is based off of Trevor Mocks Youtube video: https://www.youtube.com/watch?v=aUi9aijvpgs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq giver syntax muligheder for at finde IDataPersistence objekter
using System.Linq;


public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private AppData appData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    //Will be able to get the instance publicly, but change it privately.
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }
        //Initialize instance
        instance = this;
    }


    //Loads the FFM score when started
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadFFM();
    }

    //constructs the AppData class.
    public void NewProfile()
    {
        this.appData = new AppData();
    }

    public void LoadFFM()
    {
        this.appData = dataHandler.Load();

        // Load any saved data from a file using the data handler
        // if no data can be loaded, initialize to a new game
        if (this.appData == null)
        {
            
            Debug.Log("No data was found. Initializing data to defaults.");
            NewProfile();
        }

        // Push the Loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(appData);
        }
        Debug.LogFormat("Loaded FFM Score = " + appData.openness, appData.conscientiousness, appData.extraversion, appData.agreeableness, appData.neuroticism);
    }


    public void SaveFFM()
    {
        // Pass the data to other scripts so they can update it
        // (again, we are passing by 'ref' (reference) because we want to modify the value, and not only read it.
        // if we had passed without 'ref' (passing by value) it would copy the data instead).

        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
             dataPersistenceObj.SaveData(ref appData);
        }

        Debug.LogFormat("Saved FFM Score = " + appData.openness, appData.conscientiousness, appData.extraversion, appData.agreeableness, appData.neuroticism);

        // - save that data to a file using the data handler
        dataHandler.Save(appData);
    }

    private void OnApplicationQuit()
    {
        SaveFFM();
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

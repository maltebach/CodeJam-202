// This script is based off of Trevor Mocks Youtube video: https://www.youtube.com/watch?v=aUi9aijvpgs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
//We are using System.IO Namespace as it allows reading and writing to files and data streams.
//See documentation https://docs.microsoft.com/en-us/dotnet/api/system.io?redirectedfrom=MSDN&view=net-6.0

//We remove Monobehaviour, as we want our DataPersistenceManager to handle the script.
public class FileDataHandler
{
    //Directory path for where we want to save the data.
    private string dataDirPath = "";

    //Name of the file that we want to save to.
    private string dataFileName = "";

    //Public constructor
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public AppData Load()
    {
        //Combines the name and location of the file.
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        AppData loadedData = null;

        // if file exist, run try/catch statement. 
        if (File.Exists(fullPath))
        {
            try
            {

                // Load serialized data to file
                string dataToLoad = "";

                // we are using FileMode.Open since we want to read from the file.
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    // StreamReader is similar to what we used in the assignment to write to an CSV file,
                    // but now we are reading the file instead.
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        // Load the file's text into the dataToLoad variable as a string. 
                        dataToLoad = reader.ReadToEnd();
                    }

                    // deserialize data from JSON format back into the C# Object 
                    loadedData = JsonUtility.FromJson<AppData>(dataToLoad);
                }
            }
            catch (Exception e)
            //Log an error to the full path
            {
                Debug.LogError("Error occured when trying to LOAD data to file: " + fullPath + "\n" + e);
            }
        }
            //return: if data exists, return it, if it doesn't, return null.
        return loadedData;
    }

    public void Save(AppData data)
    {
        // we could use "dataDirPath + "/" + dataFileName"
        // but since different operating systems have different file seperaters "/", we can use the following: 
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // create directory path in case it doesn't already exist 
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize  the C# game data object into JSON
            // (Which is the file type we want to write - like .docs or .xml)
            // true is used to format the data
            string dataToStore = JsonUtility.ToJson(data, true);

            // write the serialized data to the file 
            // ('using' ensures that the connection to the file is closed
            // once the data is done being written or read)
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                // StreamWriter is the same thing we used in the assignment to write to an CSV file.
                using (StreamWriter writer  = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        //Log an error to the full path
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }


}

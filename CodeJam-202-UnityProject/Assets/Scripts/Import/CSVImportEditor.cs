using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CSVImportEditor : EditorWindow
{
    public CSVreader reader;

    public CSVlikert likert;

    [MenuItem("CSVImport/Display CSVImport Window")]
    static void Initialize()
    {
        CSVImportEditor window = (CSVImportEditor)EditorWindow.GetWindow(typeof(CSVImportEditor), true, "CSVImport");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Import Events"))
        {
            if(reader == null)
            {
                reader = FindObjectOfType<CSVreader>();
            }
            reader.ReadCSV();
        }

        if (GUILayout.Button("Import Likert questions"))
        {
            if (likert == null)
            {
                likert = FindObjectOfType<CSVlikert>();
            }
            likert.ReadCSV();
        }
    }


}

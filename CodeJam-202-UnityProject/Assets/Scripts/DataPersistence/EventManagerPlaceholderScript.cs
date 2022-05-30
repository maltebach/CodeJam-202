using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerPlaceholderScript : MonoBehaviour, IDataPersistence
{
    private bool saved = true;
    public SpriteRenderer visual;
  

    [Header("Right click component to generate guid")]
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]

    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    private void Awake()
    {
        visual = this.GetComponent<SpriteRenderer>();
    }


    public void LoadData(AppData data)
    {
        data.eventsSaved.TryGetValue(id, out saved);
        if(saved)
        {
            visual.gameObject.SetActive(false);
        }
    }

    public void SaveData(ref AppData data)
    {
        if (data.eventsSaved.ContainsKey(id))
        {
            data.eventsSaved.Remove(id);
        }
        data.eventsSaved.Add(id, saved);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!saved)
        {
            saveEvent();
        }
    }

    private void saveEvent()
    {
        saved = true;
    }
}

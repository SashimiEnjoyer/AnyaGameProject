using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{
    [SerializeField] string entityIdentifier = string.Empty;
    
    public string entityId => entityIdentifier;

    public object CaptureData()
    {
        var state = new Dictionary<string, object>();

        foreach(var saveable in GetComponents<ISaveSystem>())
        {
            state[saveable.GetType().ToString()] = saveable.CaptureSavedData();
        }

        return state;
    }

    public void RestoreData(object _data)
    {
        var stateDictionary = (Dictionary<string, object>)_data;

        foreach(var saveable in GetComponents<ISaveSystem>())
        {
            string typeName = saveable.GetType().ToString();

            if(stateDictionary.TryGetValue(typeName, out object value))
            {
                saveable.RestoreSavedData(value);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    private string savePath => $"{Application.persistentDataPath}/save.txt";

    [ContextMenu("Test Save")]
    public void DoSave()
    {
        var state = LoadFromFile();
        CaptureData(state);
        SaveToFile(state);
        Debug.Log(savePath);
    }

    [ContextMenu("Test Load")]
    public void DoLoad()
    {
        var state = LoadFromFile();
        RestoreData(state);
    }

    private void SaveToFile(object _data)
    {
        using(var stream = File.Open(savePath, FileMode.OpenOrCreate))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, _data);
        }
    }

    private Dictionary<string,object> LoadFromFile()
    {
        if (!File.Exists(savePath))
        {
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(savePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    private void CaptureData(Dictionary<string, object> _data)
    {
        foreach(var saveableData in FindObjectsOfType<SaveableEntity>())
        {
            _data[saveableData.entityId] = saveableData.CaptureData();
        }
    }

    private void RestoreData(Dictionary<string, object> _data)
    {
        foreach(var saveableData in FindObjectsOfType<SaveableEntity>())
        {
            if(_data.TryGetValue(saveableData.entityId, out object value))
            {
                saveableData.RestoreData(value);
            }
        }
    }
}

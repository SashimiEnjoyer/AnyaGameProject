using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, ISaveSystem
{
    public static PlayerStats instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public float playerHealth;
    public Transform currentCheckpoint;

    public bool playerIsDie
    {
        get { return playerHealth <= 0 ? true : false; }
    }

    public object CaptureSavedData()
    {
        return new SaveData
        {
            saveHealth = playerHealth
        };
    }

    public void RestoreSavedData(object _savedData)
    {
        var saveData = (SaveData) _savedData;
        playerHealth = saveData.saveHealth;
    }

    [System.Serializable]
    struct SaveData
    {
        public float saveHealth;
    }
}

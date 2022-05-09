using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTracker : MonoBehaviour, ISaveSystem
{
    public static InGameTracker instance;
    public bool isPause;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public object CaptureSavedData()
    {
        throw new System.NotImplementedException();
    }

    public void RestoreSavedData(object SavedData)
    {
        throw new System.NotImplementedException();
    }

}

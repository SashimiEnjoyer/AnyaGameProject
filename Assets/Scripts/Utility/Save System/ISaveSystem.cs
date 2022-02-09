using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveSystem
{
    object CaptureSavedData();

    void RestoreSavedData(object SavedData);
}

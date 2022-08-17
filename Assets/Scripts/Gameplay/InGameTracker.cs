using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameplayState { Playing, Pause, Dialogue, Stop}

public class InGameTracker : MonoBehaviour, ISaveSystem
{
    public static InGameTracker instance;

    public Action<GameplayState> onGameStateChange;

    private GameplayState _gameState;

    public GameplayState gameState
    {
        set 
        {
            if (value == _gameState)
                return;
            
            _gameState = value;
            onGameStateChange?.Invoke(value);
            Debug.Log("Game State: " + _gameState);
        }

        get
        {
            return _gameState;
        }
    }

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

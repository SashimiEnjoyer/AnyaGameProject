using UnityEngine;
using System;

public enum GameplayState { Playing, Pause, Dialogue, Stop}

[Serializable]
public struct InGameProgressTracker
{
    public GameObject[] hideShowObject;
    public bool isDone;
}

public class InGameTracker : MonoBehaviour, ISaveSystem
{
    public static InGameTracker instance;

    public InGameProgressTracker[] progressTracker;

    public Action<GameplayState> onGameStateChange;

    [SerializeField] private GameplayState _gameState;

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

    public void ChangeGameState(GameplayState state)
    {
        gameState = state;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    public void ExecuteProgressEvent(int index)
    {
        if (progressTracker[index].isDone)
            return;

        if (progressTracker[index].hideShowObject != null)
        {
            foreach (var item in progressTracker[index].hideShowObject)
            {
                item.SetActive(false);
            }
        }

        progressTracker[index].isDone = true;
    }

    [ContextMenu("Play")]
    public void TestPlaying()
    {
        gameState = GameplayState.Playing;
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

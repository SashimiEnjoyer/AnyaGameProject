using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    public static void LoadScene(string sceneName, Action onStart = null)
    {
        onStart?.Invoke();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!operation.isDone)
        {
            
        }
    }
}

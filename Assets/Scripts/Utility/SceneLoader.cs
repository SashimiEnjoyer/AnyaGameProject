using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public static class SceneLoader 
{
    public static async void LoadScene(string sceneName, Action onStart = null, Action<float> onProgress = null)
    {
        onStart?.Invoke();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        //operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            onProgress?.Invoke(operation.progress * 100);
            
            if(operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            await UniTask.Yield();
        }
    }
}

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlaySceneTool : MonoBehaviour
{
    [MenuItem("Play Scene Tool /==== Play From Start ====", false, 100)]
    private static void OpenAndPlayStartScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Main Menu.unity");
        EditorApplication.isPlaying = true;
    }
}

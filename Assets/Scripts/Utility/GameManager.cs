using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private InGameInput input;
    [SerializeField] private SoundsOnSceneManager soundsOnSceneManager;
    [SerializeField] private CameraImpulseManager cameraImpulseManager;
    [SerializeField] private SceneTransitionManager sceneTransitionManager;
    public InGameInput Input => input;
    public SoundsOnSceneManager SoundsOnSceneManager => soundsOnSceneManager;
    public CameraImpulseManager CameraImpulseManager => cameraImpulseManager;
    public SceneTransitionManager SceneTransitionManager => sceneTransitionManager;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
        
        input.Initialize();
    }
}

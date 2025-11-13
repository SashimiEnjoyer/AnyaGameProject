using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PersistentGameObject : MonoBehaviour
{
    public static PersistentGameObject instance;

    void Awake()
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
    }
}

using UnityEngine;

public class LevelSectionManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject sectionCamera;

    public void InitializeSection()
    {
        gameObject.SetActive(true);
        sectionCamera.SetActive(true);
        
        Debug.Log("Level Section Initialized: " + gameObject.name);
    }

    public void CleanupSection()
    {
        sectionCamera.SetActive(false);
        gameObject.SetActive(false);

        Debug.Log("Level Section Cleaned Up: " + gameObject.name);
    }
    
    public void SetNextSection(int nextIdx)
    {
        levelManager.SetCurrentSectionIndex(nextIdx);
    }
}

using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelSectionManager[] levelSections;

    private int currentSectionIndex = 0;
    public int CurrentSectionIndex => currentSectionIndex;

    public void SetCurrentSectionIndex(int index)
    {
        if(index >= 0 && index < levelSections.Length)
        {
            if(currentSectionIndex != index)
            {
                levelSections[index].InitializeSection();
                currentSectionIndex = index;
            }
        }
        else
        {
            Debug.LogWarning("Invalid section index set in LevelManager.");
        }
    }
}

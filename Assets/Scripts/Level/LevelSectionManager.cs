using System.Collections.Generic;
using UnityEngine;

public class LevelSectionManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject sectionCamera;
    public List<EnemyController> enemyObjects = new();
    public float spawnInterval;

    public void InitializeSection()
    {
        gameObject.SetActive(true);
        sectionCamera.SetActive(true);

        if (enemyObjects.Count > 0)
        {
            foreach (var item in enemyObjects)
            {
                item.gameObject.SetActive(true);
                item.ResetPosition();
            }
        }
    }

    [ContextMenu("Check Fill Enemies Inside Section")]
    public void CheckFillEnemiesInsideSection()
    {
        enemyObjects.Clear();
        enemyObjects.AddRange(GetComponentsInChildren<EnemyController>());
    }

    public void CleanupSection()
    {
        sectionCamera.SetActive(false);
        gameObject.SetActive(false);

        foreach (var item in enemyObjects)
        {
            item.ResetPosition();
            item.gameObject.SetActive(false);
        }
    }
    
    public void SetNextSection(int nextIdx)
    {
        levelManager.SetCurrentSectionIndex(nextIdx);
    }
}

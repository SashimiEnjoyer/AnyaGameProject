using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemySpawnerDetail
{
    public GameObject enemyPrefab;
    public Transform[] spawnPos;
}

public class EnemySpawnManager : MonoBehaviour
{
    public EnemySpawnerDetail rangedEnemyDetail;
    public EnemySpawnerDetail patrolEnemyDetail;
    public int spawnCount = 5;  

    [SerializeField] List<GameObject> RangedEnemy = new List<GameObject>();
    [SerializeField] List<GameObject> PatrolEnemy = new List<GameObject>();
    int rangedEnemyTracker = 0;
    int patrolEnemyTracker = 0; 

    private void Awake()
    {
        InitiateEnemies(0);
    }

    private void Start()
    {
        SpawnEnemy1();
        SpawnEnemy2();
    }

    private void OnDisable()
    {
        UnsetEnemy();
    }

    public void InitiateEnemies(int index)
    {

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject go = Instantiate(rangedEnemyDetail.enemyPrefab);
            go.GetComponentInChildren<EnemyController>().onEnemyDied += WhenEnemy1Died;
            go.SetActive(false);
            RangedEnemy.Add(go);
            
            go = Instantiate(patrolEnemyDetail.enemyPrefab);
            go.GetComponentInChildren<EnemyController>().onEnemyDied += WhenEnemy2Died;
            go.SetActive(false);
            PatrolEnemy.Add(go);
        }
    }

    public void SpawnEnemy1()
    {
        RangedEnemy[rangedEnemyTracker].transform.position = rangedEnemyDetail.spawnPos[Random.Range(0, rangedEnemyDetail.spawnPos.Length)].position;
        RangedEnemy[rangedEnemyTracker].SetActive(true);
        rangedEnemyTracker++;
    }

    public void SpawnEnemy2()
    {
        PatrolEnemy[patrolEnemyTracker].transform.position = patrolEnemyDetail.spawnPos[Random.Range(0, patrolEnemyDetail.spawnPos.Length)].position;
        PatrolEnemy[patrolEnemyTracker].SetActive(true);
        patrolEnemyTracker++;
    }

    public void UnsetEnemy()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            RangedEnemy[i].GetComponent<EnemyController>().onEnemyDied -= WhenEnemy1Died;
            PatrolEnemy[i].GetComponent<EnemyController>().onEnemyDied -= WhenEnemy2Died;
        }
    }

    public void WhenEnemy1Died()
    {
        if(rangedEnemyTracker < spawnCount)
            SpawnEnemy1();
    }

    public void WhenEnemy2Died()
    {
        if(patrolEnemyTracker < spawnCount)
            SpawnEnemy2();
    }

}

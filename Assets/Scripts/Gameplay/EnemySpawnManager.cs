using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnerDetail
{
    public GameObject enemyPrefab;
    public Transform[] spawnPos;
    public int spawnCounter;
    public List<GameObject> currentWave = new();
}

public class EnemySpawnManager : MonoBehaviour
{
    public EnemySpawnerDetail rangedEnemyDetail = new();
    public EnemySpawnerDetail patrolEnemyDetail = new();
    public EnemySpawnerDetail bringerOfDeathDetail = new();
    public int maxSpawnCounter = 5;

    public static Action OnWaveEnded;

    private void Awake()
    {
        InitiatePool();
    }

    private void Start()
    {
        InitiateWave();
    }

    private void OnDisable()
    {
        UnsetEnemy();
    }

    public void InitiatePool()
    {

        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(rangedEnemyDetail.enemyPrefab);
            go.GetComponentInChildren<EnemyController>().onEnemyDied += () => WhenEnemyDied(rangedEnemyDetail);
            go.SetActive(false);
            rangedEnemyDetail.currentWave.Add(go);
            
            go = Instantiate(patrolEnemyDetail.enemyPrefab);
            go.GetComponentInChildren<EnemyController>().onEnemyDied += () => WhenEnemyDied(patrolEnemyDetail);
            go.SetActive(false);
            patrolEnemyDetail.currentWave.Add(go);

            go = Instantiate(bringerOfDeathDetail.enemyPrefab);
            go.GetComponentInChildren<EnemyController>().onEnemyDied += () => WhenEnemyDied(bringerOfDeathDetail);
            go.SetActive(false);
            bringerOfDeathDetail.currentWave.Add(go);
        }
    }

    public void InitiateWave()
    {
        SpawnEnemy(rangedEnemyDetail);
        SpawnEnemy(patrolEnemyDetail);
        SpawnEnemy(bringerOfDeathDetail);
    }

    public void SpawnEnemy(EnemySpawnerDetail detail)
    {
        detail.currentWave[detail.spawnCounter].transform.position = detail.spawnPos[UnityEngine.Random.Range(0, detail.spawnPos.Length)].position;
        detail.currentWave[detail.spawnCounter].SetActive(true);
        detail.spawnCounter++;
    }

    public void UnsetEnemy()
    {
        for (int i = 0; i < 10; i++)
        {
            rangedEnemyDetail.currentWave[i].GetComponent<EnemyController>().onEnemyDied -= () => WhenEnemyDied(rangedEnemyDetail);
            patrolEnemyDetail.currentWave[i].GetComponent<EnemyController>().onEnemyDied -= () => WhenEnemyDied(patrolEnemyDetail);
            bringerOfDeathDetail.currentWave[i].GetComponent<EnemyController>().onEnemyDied -= () => WhenEnemyDied(bringerOfDeathDetail);
        }
    }

    public void WhenEnemyDied(EnemySpawnerDetail detail)
    {
        if(detail.spawnCounter < maxSpawnCounter)
        {
            SpawnEnemy(detail);
        }

        CheckEndofWave();
    }

    private void CheckEndofWave()
    {
        if (rangedEnemyDetail.spawnCounter >= maxSpawnCounter
            && patrolEnemyDetail.spawnCounter >= maxSpawnCounter
            && bringerOfDeathDetail.spawnCounter >= maxSpawnCounter)
            OnWaveEnded?.Invoke();
    }
}

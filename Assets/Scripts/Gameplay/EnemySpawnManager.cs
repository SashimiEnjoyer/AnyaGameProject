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
    public GameObject simpleEnemyType;
    public bool simpleTypeDeployed = false;
    public int maxSpawnCounter = 5;
    public Action OnEnemyDied;

    private void Awake()
    {
        InitiatePool();
    }

    //private void Start()
    //{
    //    InitiateWave();
    //}

    private void OnDisable()
    {
        UnsetEnemy();
    }

    public void InitiatePool()
    {

        for (int i = 0; i < maxSpawnCounter; i++)
        {
            GameObject go = Instantiate(rangedEnemyDetail.enemyPrefab);
            go.GetComponentInChildren<EnemyController>().onEnemyDied += () => WhenEnemyDied(rangedEnemyDetail);
            go.SetActive(false);
            rangedEnemyDetail.currentWave.Add(go);
            go = null;
            
            go = Instantiate(patrolEnemyDetail.enemyPrefab);
            go.GetComponentInChildren<EnemyController>().onEnemyDied += () => WhenEnemyDied(patrolEnemyDetail);
            go.SetActive(false);
            patrolEnemyDetail.currentWave.Add(go);
            go = null;

            go = Instantiate(bringerOfDeathDetail.enemyPrefab);
            go.GetComponentInChildren<EnemyController>().onEnemyDied += () => WhenEnemyDied(bringerOfDeathDetail);
            go.SetActive(false);
            bringerOfDeathDetail.currentWave.Add(go);
            go = null;
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
        detail.currentWave[detail.spawnCounter].transform.position = detail.spawnPos[detail.spawnCounter % 2].position;
        detail.currentWave[detail.spawnCounter].SetActive(true);
        detail.spawnCounter++;
    }

    public void UnsetEnemy()
    {
        for (int i = 0; i < maxSpawnCounter; i++)
        {
            rangedEnemyDetail.currentWave[i].GetComponent<EnemyController>().onEnemyDied -= () => WhenEnemyDied(rangedEnemyDetail);
            patrolEnemyDetail.currentWave[i].GetComponent<EnemyController>().onEnemyDied -= () => WhenEnemyDied(patrolEnemyDetail);
            bringerOfDeathDetail.currentWave[i].GetComponent<EnemyController>().onEnemyDied -= () => WhenEnemyDied(bringerOfDeathDetail);
        }
    }

    public void WhenEnemyDied(EnemySpawnerDetail detail)
    {
        OnEnemyDied?.Invoke();
        CheckEndofWave();

        if(patrolEnemyDetail.spawnCounter == maxSpawnCounter && !simpleTypeDeployed)
        {
            simpleEnemyType.SetActive(true);
            simpleTypeDeployed = true;
        }

        if(detail.spawnCounter == 3)
        {
            SpawnEnemy(detail);
        }

        if(detail.spawnCounter < maxSpawnCounter)
        {
            SpawnEnemy(detail);
        }

    }

    private void CheckEndofWave()
    {
        if (rangedEnemyDetail.spawnCounter >= maxSpawnCounter
            && patrolEnemyDetail.spawnCounter >= maxSpawnCounter
            && bringerOfDeathDetail.spawnCounter >= maxSpawnCounter)
        {
            InGameTracker.instance.onWinEnding?.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateId
{
    Idle,
    Hit,
    Patrol,
    Aggro
}

public interface EnemyState
{
    EnemyStateId GetId();
    void Enter(Enemy enemy);
    void Update(Enemy enemy);
    void FixedUpdate(Enemy enemy);
    void Exit(Enemy enemy);
}

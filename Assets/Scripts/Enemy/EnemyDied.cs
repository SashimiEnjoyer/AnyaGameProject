using DG.Tweening;
using UnityEngine;

public class EnemyDied : CharacterState
{
    EnemyController controller;
    public EnemyDied(EnemyController _character) : base(_character)
    {
        controller = _character;
    }

    public override void EnterState()
    {
        Debug.Log("Enter Die State!");
        baseEnemy.onEnemyDied?.Invoke();
        baseEnemy.Died();
    }
}

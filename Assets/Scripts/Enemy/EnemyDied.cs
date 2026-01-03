using Animancer;
using UnityEngine;

public class EnemyDied : CharacterState
{
    PatrolTypeEnemy en;
    AnimancerState state;

    public EnemyDied(PatrolTypeEnemy _character) : base(_character)
    {
        en = _character;
    }

    public override void EnterState()
    {
        Debug.Log("Enter Die State!");

        state = baseEnemy.AnimancerComponent.Play(en.diedClip);
        state.Events(this).OnEnd ??= OnEnd;

    }

    void OnEnd() 
    {
        baseEnemy.onEnemyDied?.Invoke();
        baseEnemy.Died();
        baseEnemy.currHealth = baseEnemy.maxHealth;
    }
}

using Animancer;
using UnityEngine;

public class EnemyDied : CharacterState
{
    EnemyController en;
    AnimancerState state;

    public EnemyDied(EnemyController _character) : base(_character)
    {
        en = _character;
    }

    public override void EnterState()
    {
        Debug.Log("Enter Die State!");

        if (en.AnimancerComponent == null)
        {
            OnEnd();
        }
        else
        {
            state = en.AnimancerComponent.Play(en.diedClip);
            state.Events(this).OnEnd ??= OnEnd;
        }
    }

    void OnEnd() 
    {
        en.onEnemyDied?.Invoke();
        en.Died();
        en.currHealth = en.maxHealth;
    }
}

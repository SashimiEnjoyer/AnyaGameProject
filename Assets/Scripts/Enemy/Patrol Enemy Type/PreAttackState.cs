using UnityEngine;

public class PreAttackState : CharacterState
{
    EnemyController en;
    public PreAttackState(EnemyController enemy) : base(enemy) 
    {
        en = enemy;
    }

    float timer = 0f;

    public override void EnterState()
    {
        en.preAttackIndicator.SetActive(true);   
        timer = Time.time + 0.5f;
    }

    public override void Tick()
    {
        if (Time.time >= timer)
        {
            
            baseEnemy.SetState(baseEnemy.attackState);
        }
    }

    public override void ExitState()
    {
        en.preAttackIndicator.SetActive(false);
    }
}

using UnityEngine;

public class EnemyHurt : CharacterState
{
    public EnemyHurt(EnemyController enemy) : base(enemy) { }

    float countdown = 0f;
    float timer = 1f;

    public override void EnterState()
    {
        if (enemy.health <= 0)
            return;  //Died State

        enemy.SetAnimatorState(enemy.anim, "Enemy_Hurt");
    }

    public override void Tick()
    {
        if (countdown < timer)
            countdown += Time.deltaTime;
        else
            enemy.SetState(enemy.patrolState);

    }
}

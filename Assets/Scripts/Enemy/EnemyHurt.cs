using UnityEngine;

public class EnemyHurt : CharacterState
{
    ParticleSystem hitEffect;
    public EnemyHurt(EnemyController enemy) : base(enemy) { }

    float timer = 0f;

    public override void EnterState()
    {
        Debug.Log("Enter Hurt State!");

        enemy.health -= 1;
        enemy.getHit = true;

        if (hitEffect == null)
        {
            hitEffect = GameObject.Instantiate(enemy.afterHitEffect, enemy.transform).GetComponent<ParticleSystem>();
            hitEffect.Play();
        }else
            hitEffect.Emit(50);

        enemy.Knocked();
        enemy.SetAnimatorState(enemy.anim, "Enemy_Hurt");
        timer = Time.time + enemy.staggerTime;
    }

    public override void Tick()
    {
        if(Time.time >= timer)
        {
            if (enemy.health > 0) 
                enemy.SetState(enemy.patrolState);
            else
                enemy.SetState(enemy.enemyDied);
        }
    }

    public override void ExitState()
    {
        enemy.rb.velocity = Vector2.zero;
        enemy.getHit = false;
    }
}

using UnityEngine;

public class EnemyHurt : CharacterState
{
    ParticleSystem hitEffect;
    public EnemyHurt(EnemyController enemy) : base(enemy) { }

    float timer = 0f;

    public override void EnterState()
    {
        Debug.Log("Enter Hurt State!");

        baseEnemy.currHealth -= 1;
        baseEnemy.getHit = true;

        if (hitEffect == null)
        {
            hitEffect = GameObject.Instantiate(baseEnemy.afterHitEffect, baseEnemy.transform).GetComponent<ParticleSystem>();
            hitEffect.Play();
        }else
            hitEffect.Emit(50);

        baseEnemy.Knocked();
        //baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy_Hurt");
        //GameManager.instance.CameraImpulseManager.ActiveEnemyImpulse();
        timer = Time.time + baseEnemy.staggerTime;
    }

    public override void Tick()
    {
        if(Time.time >= timer)
        {
            if (baseEnemy.currHealth > 0) 
                baseEnemy.SetState(baseEnemy.defaultState);
            else
                baseEnemy.SetState(baseEnemy.enemyDied);
        }
    }

    public override void ExitState()
    {
        baseEnemy.rb.linearVelocity = Vector2.zero;
        baseEnemy.getHit = false;
    }
}

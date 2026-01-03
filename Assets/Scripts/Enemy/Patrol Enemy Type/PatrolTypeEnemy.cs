using Animancer;
using UnityEngine;

public class PatrolTypeEnemy : EnemyController
{

    private void Awake()
    {
        AssignPlayerTransform();
        rb = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<CapsuleCollider2D>();
        
        defaultState = new PatrolType.PatrolState(this);
        chaseState = new PatrolType.ChaseState(this);
        attackState = new PatrolType.AttackState(this);
        enemyHurted = new EnemyHurt(this);
        enemyDied = new EnemyDied(this);
        enemyPause = new EnemyPause(this);

        if(usePreAttack)
            preAttackState = new PreAttackState(this);

        currHealth = maxHealth;
    }

    private void OnEnable()
    {
        SetState(defaultState);
    }

    public override void EnemyDoAttack()
    {
        if (PlayerTouched() && !getHit && PatrolAttack == true)
        {
            PlayerTouched().collider.GetComponent<PlayerController>().PlayerHurt(transform.position, 1);
        }
    }

    public override void Move(float multiplier = 1f)
    {
        rb.linearVelocity = new Vector2(movementSpeed * CurrentDirection * multiplier, rb.linearVelocity.y);
    }

    public override void StopMove()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public override void Died()
    {
        //Destroy(transform.parent.gameObject, 1f);
        transform.parent.gameObject.SetActive(false);
    }

    public override void Flip()
    {
        if (getHit)
            return;

        //isFacingRight = !isFacingRight;
        CurrentDirection *= -1;
        StopMove();
        transform.Rotate(0, 180f, 0);
    }


    public override void Knocked()
    {
        StopMove();
        float playerDirX = PlayerDirection().x < 0 ? -1f : 1f;
        float xRes = Random.Range(knockDistance.x, knockDistance.x + 5f);
        float yRes = Random.Range(knockDistance.y, knockDistance.y + 5f);

        rb.AddForce(new Vector2(-playerDirX * xRes, yRes));
    }

    public override void EnemyHurted()
    {
        if(staggerTime <= 0)
        {
            currHealth -= 1;
            getHit = true;

            ParticleSystem hitEffect = null;

            if (hitEffect == null)
            {
                hitEffect = GameObject.Instantiate(afterHitEffect, transform).GetComponent<ParticleSystem>();
                hitEffect.Play();
            }
            else
                hitEffect.Emit(50);

            Knocked();
            //GameManager.instance.CameraImpulseManager.ActiveEnemyImpulse();
            getHit = false;

            if (currHealth <= 0)
                SetState(enemyDied);

            return;
        }

        if (getHit)
            return;

        SetState(enemyHurted);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 xStart = transform.position + Vector3.left * idleToChaseTriggerDistance.x;
        Vector3 xEnd = transform.position + Vector3.right * idleToChaseTriggerDistance.x;
        Gizmos.DrawLine(xStart, xEnd);

        // Draw the Y-Axis (Vertical)

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseToAttackTriggerDistance);

        Gizmos.color = Color.green;
        Vector3 yStart = transform.position + Vector3.down * idleToChaseTriggerDistance.y;
        Vector3 yEnd = transform.position + Vector3.up * idleToChaseTriggerDistance.y;
        Gizmos.DrawLine(yStart, yEnd);
    }

    //public override void ResetPosition()
    //{
    //    transform.position = startingPoint.position;

    //    //if (CurrentDirection != 1)
    //    //{
    //    //    Flip();
    //    //    CurrentDirection = 1;
    //    //}
    //}

}

using UnityEngine;

public class PatrolTypeEnemy : EnemyController
{
    Vector2 playerPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        patrolState = new PatrolType.PatrolState(this);
        chaseState = new PatrolType.ChaseState(this);
        attackState = new PatrolType.AttackState(this);
        enemyHurted = new EnemyHurt(this);
        enemyDied = new EnemyDied(this);
    }

    private void Start()
    {
        SetState(patrolState);
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
        rb.velocity = new Vector2(movementSpeed * CurrentDirection * multiplier, rb.velocity.y);
    }

    public override void StopMove()
    {
        rb.velocity = Vector2.zero;
    }

    public override void Died()
    {
        Destroy(transform.parent.gameObject, 1f);
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
        rb.AddForce(new Vector2(-PlayerDirection().x * knockDistance.x, knockDistance.y));
    }

    public override void EnemyHurted()
    {

        SetState(enemyHurted);

        if (afterHitEffect == null)
        {
            Debug.LogWarning("No Effect Prefabs Assigned");
            return;
        }
    }

    public override void ResetPosition()
    {
        transform.position = startingPoint.position;

        //if (CurrentDirection != 1)
        //{
        //    Flip();
        //    CurrentDirection = 1;
        //}
    }

}

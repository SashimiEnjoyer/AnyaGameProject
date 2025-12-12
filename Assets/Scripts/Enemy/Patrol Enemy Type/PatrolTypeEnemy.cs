using UnityEngine;

public class PatrolTypeEnemy : EnemyController
{
    [Header("Patrol Enemy References")]
    public EnemyAggroStatus AggroStatus;
    public GameObject attackHitBox;
    public float preAttackTimer;
    public float attackTimer;
    public float minAttackTriggerRange;
    public float minPatrolTime;
    public float maxPatrolTime;

    private void Awake()
    {
        AssignPlayerTransform();
        rb = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<CapsuleCollider2D>();
        //anim = GetComponent<Animator>();

        defaultState = new PatrolType.PatrolState(this);
        chaseState = new PatrolType.ChaseState(this);
        attackState = new PatrolType.AttackState(this);
        enemyHurted = new EnemyHurt(this);
        enemyDied = new EnemyDied(this);
        enemyPause = new EnemyPause(this);

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
        if (getHit)
            return;

        Debug.LogWarning("Hit By The Player");

        SetState(enemyHurted);

        if (afterHitEffect == null)
        {
            Debug.LogWarning("No Effect Prefabs Assigned");
            return;
        }
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

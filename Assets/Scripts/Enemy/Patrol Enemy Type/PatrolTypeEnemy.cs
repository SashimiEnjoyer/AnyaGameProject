using UnityEngine;

public class PatrolTypeEnemy : EnemyController
{

    Vector2 playerPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        patrolState = new PatrolEnemy_PatrolState(this);
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

    public override void Move()
    {
        rb.velocity = new Vector2(isFacingRight? movementSpeed : -movementSpeed, rb.velocity.y);
    }

    public override void Died()
    {
        Destroy(transform.parent.gameObject, 1f);
    }

    public override void Flip()
    {
        if (getHit)
            return;

        if (isFacingRight)
        {
            isFacingRight = false;
            transform.Rotate(0, 180f, 0);
        }
        else
        {
            isFacingRight = true;
            transform.Rotate(0, 180f, 0);
        }
    }

    public override void Knocked()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(playerPos.x > transform.position.x ? -knockDistance.x : knockDistance.x, knockDistance.y));
    }

    public override void EnemyHurted(Vector2 _target)
    {
        playerPos = _target;

        SetState(enemyHurted);

        if (afterHitEffect == null)
        {
            Debug.LogWarning("No Effect Prefabs Assigned");
            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            onTouchBorder?.Invoke();
        }

    }

}

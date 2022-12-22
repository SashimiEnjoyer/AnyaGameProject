using UnityEngine;

public class PatrolTypeEnemy : EnemyController
{
    private void Awake()
    {
        patrolState = new PatrolEnemy_PatrolState(this);
        enemyHurted = new EnemyHurt(this);
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

    public override void move()
    {
        rb.velocity = new Vector2(isFacingRight? movementSpeed : -movementSpeed, rb.velocity.y);
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

    public override void EnemyHurted(Vector2 _target)
    {
        getHit = true;
        rb.AddForce(new Vector2(_target.x > transform.position.x ? -350 : 350, 100));
        health -= 1;

        if (health <= 0)
        {
            Destroy(transform.parent.gameObject, 0.5f);
        }

        SetState(enemyHurted);

        if (afterHitEffect == null)
        {
            Debug.LogWarning("No Effect Prefabs Assigned");
            return;
        }

        GameObject go = Instantiate(afterHitEffect, transform.position, Quaternion.identity);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            onTouchBorder?.Invoke();
        }

    }

}

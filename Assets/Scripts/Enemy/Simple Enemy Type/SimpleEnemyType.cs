using UnityEngine;

public class SimpleEnemyType : EnemyController
{
    public bool isWalkRight;
    public float liveTime = 5f;

    private void Awake()
    {
        defaultState = new SimpleEnemy.AttackState(this);
        enemyHurted = new EnemyHurt(this);
        enemyDied = new EnemyDied(this);
        enemyPause = new EnemyPause(this);
    }

    private void OnEnable()
    {
        SetState(defaultState);
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

    public override void Move(float multiplier = 1f)
    {
        rb.velocity = new Vector2(movementSpeed * CurrentDirection * multiplier, rb.velocity.y);
    }

    public override void EnemyHurted()
    {
        if (getHit)
            return;

        SetState(enemyHurted);

        if (afterHitEffect == null)
        {
            Debug.LogWarning("No Effect Prefabs Assigned");
            return;
        }
    }

    public override void Knocked()
    {
        rb.AddForce(new Vector2(0, knockDistance.y));
    }

    public override void Died()
    {
        //Destroy(gameObject, 1f);
        gameObject.SetActive(false);
    }
}

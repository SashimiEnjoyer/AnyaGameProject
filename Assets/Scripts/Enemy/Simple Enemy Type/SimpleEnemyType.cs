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
        rb.linearVelocity = new Vector2(movementSpeed * CurrentDirection * multiplier, rb.linearVelocity.y);
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
        float playerDirX = PlayerDirection().x < 0 ? -1f : 1f;
        float xRes = Random.Range(knockDistance.x, knockDistance.x + 5f);
        float yRes = Random.Range(knockDistance.y, knockDistance.y + 5f);

        rb.AddForce(new Vector2(-playerDirX * xRes, yRes));
    }

    public override void Died()
    {
        //Destroy(gameObject, 1f);
        gameObject.SetActive(false);
    }
}

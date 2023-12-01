using UnityEngine;

public class RangeTypeEnemy : EnemyController
{
    [Header("Range Enemy References")]
    public Transform projectilePos;
    public GameObject projectile;
    public float projectileSpeed = 10f;
    public float nextProjectileTimer = 3f;
    public float spawnTimerAfterAnimation = 0.5f;

    private void Awake()
    {
        defaultState = new RangeType.ChaseState(this);
        attackState = new RangeType.AttackState(this);
        enemyHurted = new EnemyHurt(this);
        enemyDied = new EnemyDied(this);
        currHealth = maxHealth;
    }
    private void Start()
    {
        AssignPlayerTransform();
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

    public void RotateTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)projectilePos.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectilePos.rotation = Quaternion.Euler(Vector3.forward * (angle));
    }

    public override void Died()
    {
        //Destroy(gameObject, 1f);
        gameObject.SetActive(false);
    }
}

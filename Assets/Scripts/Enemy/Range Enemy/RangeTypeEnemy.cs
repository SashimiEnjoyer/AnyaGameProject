using UnityEngine;

public class RangeTypeEnemy : EnemyController
{

    private void Awake()
    {
        defaultState = new PatrolType.PatrolState(this);
        chaseState = new PatrolType.ChaseState(this);
        attackState = new RangeType.AttackState(this);
        enemyHurted = new EnemyHurt(this);
        enemyDied = new EnemyDied(this);
        enemyPause = new EnemyPause(this);

        if (usePreAttack)
            preAttackState = new PreAttackState(this);

        currHealth = maxHealth;
    }
    private void OnEnable()
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

    public override void Died()
    {
        //Destroy(gameObject, 1f);
        gameObject.SetActive(false);
    }
}

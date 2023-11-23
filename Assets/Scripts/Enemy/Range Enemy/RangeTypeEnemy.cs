using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTypeEnemy : EnemyController
{
    public Transform projectilePos;
    public GameObject projectile;

    private void Awake()
    {
        chaseState = new RangeType.ChaseState(this);
        attackState = new RangeType.AttackState(this);
    }
    private void Start()
    {
        SetState(chaseState);
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
}

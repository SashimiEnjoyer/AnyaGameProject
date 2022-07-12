using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTypeEnemy : EnemyController
{
    private void Start()
    {
        SetState(new EnemyAttackState(this));
    }

    public override void EnemyAttacking()
    {
        if (player() && !getHit)
        {
            player().collider.GetComponent<PlayerController>().PlayerAttacked(transform.position, 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            Flip();
        }

    }

}

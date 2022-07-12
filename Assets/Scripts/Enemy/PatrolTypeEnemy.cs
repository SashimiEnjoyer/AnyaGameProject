using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTypeEnemy : EnemyController
{
    private void Start()
    {
        SetState(new PatrolEnemyAttackState(this));
    }

    public override void EnemyAttacking()
    {
        if (player() && !getHit)
        {
            player().collider.GetComponent<PlayerController>().PlayerAttacked(transform.position, 1);
        }

    }

    public override void Move()
    {
        if (rb != null)
            rb.velocity = new Vector2(isFacingRight ? movementSpeed : -movementSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border") && !getHit)
        {
            Flip();
        }

    }

}

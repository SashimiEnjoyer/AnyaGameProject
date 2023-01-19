using UnityEngine;

public class EnemyPatrol_State : EnemyState
{
    private bool mustTurn = false;

    public EnemyStateId GetId()
    {
        return EnemyStateId.Patrol;
    }

    public void Enter(Enemy enemy)
    {
        enemy.animator.SetBool("isWalking", true);
    }

    public void FixedUpdate(Enemy enemy)
    {
        mustTurn = !Physics2D.OverlapCircle(enemy.groundCheckPos.position, 0.1f, enemy.platformLayer);
    }

    public void Update(Enemy enemy)
    {
        if (enemy.isCollideWithWall() || enemy.isCollideWithEnemyAnchor() || mustTurn)
        {
            enemy.Flip();
        }
        enemy.Move();
    }

    public void Exit(Enemy enemy)
    {
    }

}

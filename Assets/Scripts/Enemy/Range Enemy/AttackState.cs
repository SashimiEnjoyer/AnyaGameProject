using UnityEngine;

public static partial class RangeType
{
    public class AttackState : CharacterState
    {
        RangeTypeEnemy enemy;
        GameObject proj;
        float interval = 0f;

        public AttackState(RangeTypeEnemy _enemy) : base(_enemy)
        {
            enemy = _enemy;
        }

        public override void EnterState()
        {
            Debug.Log("Attack State!");
            proj = Object.Instantiate(enemy.projectile, enemy.projectilePos);
            proj.transform.SetParent(null);
            proj.transform.position = enemy.projectilePos.position;
            proj.GetComponent<ProjectileMove>().MoveProjectile(0.5f, 4);
            interval = Time.time + 2;
            enemy.SetState(enemy.chaseState);
        }
    }
}


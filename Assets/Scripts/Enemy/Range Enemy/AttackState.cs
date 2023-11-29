using UnityEngine;

public static partial class RangeType
{
    public class AttackState : CharacterState
    {
        RangeTypeEnemy currEnemy;
        GameObject proj;
        float interval = 0f;

        public AttackState(RangeTypeEnemy _enemy) : base(_enemy)
        {
            currEnemy = _enemy;
        }

        public override void EnterState()
        {
            Debug.Log("Attack State!");
            proj = Object.Instantiate(currEnemy.projectile, currEnemy.projectilePos);
            proj.transform.SetParent(null);
            proj.transform.position = currEnemy.projectilePos.position;
            proj.GetComponent<ProjectileMove>()?.MoveProjectile(currEnemy.projectileSpeed, 4);
            interval = Time.time + 1;
        }

        public override void Tick()
        {
            if (Time.time > interval)
            {
                baseEnemy.SetState(baseEnemy.defaultState);
                interval = 0f;
            }
        }
    }
}


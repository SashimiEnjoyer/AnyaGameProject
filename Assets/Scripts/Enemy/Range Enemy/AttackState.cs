using UnityEngine;

public static partial class RangeType
{
    public class AttackState : CharacterState
    {
        RangeTypeEnemy currEnemy;
        GameObject proj;
        float interval = 0.5f;

        public AttackState(RangeTypeEnemy _enemy) : base(_enemy)
        {
            currEnemy = _enemy;
        }

        public override void EnterState()
        {
            Debug.Log("Attack State!");
            baseEnemy.SetAnimatorState(baseEnemy.anim, "Enemy_Attack");
            interval = Time.time + currEnemy.spawnTimerAfterAnimation;

            if (proj != null)
            {
                proj.transform.parent = currEnemy.projectilePos;
                proj.transform.eulerAngles = currEnemy.projectilePos.eulerAngles;
                proj.transform.position = Vector3.zero;
            }
        }

        public override void Tick()
        {
            currEnemy.RotateTowards(baseEnemy.playerTransform.position);

            if (Mathf.Sign(baseEnemy.CurrentDirection) != Mathf.Sign(baseEnemy.PlayerDirection().x))
            {
                baseEnemy.Flip();
            }

            if (Time.time > interval)
            {
                baseEnemy.SetState(baseEnemy.defaultState);
            }
        }

        public override void ExitState()
        {
            if(proj == null)
                proj = Object.Instantiate(currEnemy.projectile, currEnemy.projectilePos);

            proj.SetActive(true);
            proj.transform.SetParent(null);
            proj.transform.position = currEnemy.projectilePos.position;
            proj.GetComponent<ProjectileMove>()?.MoveProjectile(currEnemy.projectileSpeed, 4);
            
        }
    }
}


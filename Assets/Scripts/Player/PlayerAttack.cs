using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : CharacterState
{
    public PlayerAttack(PlayerController player) : base(player)
    {
    }

    float animationTimer = 0f;
    float time = 0f;
    GameObject slashEffectObject;

    public override void EnterState()
    {
        if (character.PlayerTouchGround(Vector2.down))
        {
            if(Mathf.Abs(character.horizontalInput) > 0)
                character.SetAnimatorState(character.anim, "Running_Attack_Ground");
            else
                character.SetAnimatorState(character.anim, "Attack_Ground");
        }else
            character.SetAnimatorState(character.anim, "Attack_Ground");

        character.attackAudio.PlayOneShot(character.attackClip[Random.Range(0, character.attackClip.Length)]);
        character.isAttacking = true;

        slashEffectObject = GameObject.Instantiate(character.attackSlashEffectPrefab, character.transform.position + new Vector3( character.isFacingRight? 2 : -2, 0), Quaternion.identity);
        slashEffectObject.transform.parent = character.transform;
    }

    public override void Tick()
    {
        if (animationTimer <= 0f)
            animationTimer = character.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        time += Time.deltaTime;

        if (time < animationTimer)
        {
            for (int i = 0; i < character.PlayerTouchEnemy(character.isFacingRight).Length; i++)
            {
                if (character.listOfEnemies.Contains(character.PlayerTouchEnemy(character.isFacingRight)[i].collider))
                    continue;
                else
                {
                    character.listOfEnemies.Add(character.PlayerTouchEnemy(character.isFacingRight)[i].collider);

                    // foreach (var enemy in character.PlayerTouchEnemy(character.isFacingRight)[i].collider.GetComponents<IEnemy>())
                    //     enemy.EnemyHurted(character.transform.position);

                    foreach (var enemy in character.PlayerTouchEnemy(character.isFacingRight)[i].collider.transform.GetComponents<Enemy>()){
                        enemy.Hurt(character.transform.position);
                    }
                        

                }
            }
        }
        else
        {
            character.SetState(new PlayerLocomotion(character));
            character.isAttacking = false;
        }

                
    }

    public override void ExitState()
    {
        character.EmptyEnemyList();
        time = 0;
        GameObject.Destroy(slashEffectObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : CharacterState
{
    public PlayerAttack(PlayerController player) : base(player)
    {
    }

    float time;

    public override void EnterState()
    {
        character.attackAudio.PlayOneShot(character.attackClip[Random.Range(0, 5)]);
        character.PlayAnimationAttack();
        character.isAttacking = true;
    }

    public override void Tick()
    {
        time += Time.deltaTime;

        if (time < character.AnimationLength("Anya_Attack1"))
        {
            for (int i = 0; i < character.PlayerTouchEnemy(character.isFacingRight).Length; i++)
            {
                if (character.listOfEnemies.Contains(character.PlayerTouchEnemy(character.isFacingRight)[i].collider))
                    continue;
                else
                {
                    character.listOfEnemies.Add(character.PlayerTouchEnemy(character.isFacingRight)[i].collider);

                    foreach (var enemy in character.PlayerTouchEnemy(character.isFacingRight)[i].collider.GetComponents<IEnemy>())
                        enemy.EnemyHurted(character.transform.position);
                        

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
    }
}

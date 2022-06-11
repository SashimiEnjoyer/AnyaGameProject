using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacked : CharacterState
{
    public PlayerAttacked(PlayerController player) : base(player) { }

    float timer = 0;

    public override void EnterState()
    {
        Debug.Log("Player Hit");

        character.invulnerableCount = 0;
        character.isInvulnerable = true;
        character.isGetHitByEnemy = true;
        //PlayerStats.instance.playerHealth -= 1;

        if (PlayerStats.instance.playerHealth <= 0)
            character.SetState(new PlayerDie(character));
    }

    public override void Tick()
    {

        if(timer < 1)
        {
            timer += Time.deltaTime;
        }else
        {
            timer = 1;
            character.SetState(new PlayerLocomotion(character));
        }    
    }

    public override void ExitState()
    {
        if (PlayerStats.instance.playerHealth > 0)
            character.isGetHitByEnemy = false;
    }
}

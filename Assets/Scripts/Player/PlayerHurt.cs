using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : CharacterState
{
    public PlayerHurt(PlayerController player) : base(player) { }
    public Rigidbody2D rb;
    float timer = 0;

    public override void EnterState()
    {

        character.PlayAnimationHurt();
        character.invulnerableCount = 0;
        character.isInvulnerable = true;
        character.isGetHitByEnemy = true;

        if (PlayerStats.instance.playerHealth <= 0)
        {
            character.SetState(new PlayerDie(character));
            character.rb.velocity = Vector2.zero;
        }
    }

    public override void Tick()
    {

        if(timer < character.AnimationLength("Anya_Hurt"))
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

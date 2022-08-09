using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : CharacterState
{

    public PlayerDash(PlayerController player) : base(player)
    {
    }


    public override void EnterState()
    {
        character.isDashing = true;
        character.anim.SetBool("Dashing2", true);
        character.anim.SetTrigger("Dashing");
    }

    public override void Tick()
    {
        character.dashCounter += Time.deltaTime;

        character.dashCounter = Mathf.Clamp(character.dashCounter, 0, character.dashTime + character.dashCooldown);
    }

    public override void PhysicTick()
    {
        
        if (character.dashCounter < character.dashTime)
        {
            character.rb.velocity = new Vector2((character.isFacingRight ? 1 : -1) * character.dashSpeed * Time.deltaTime, 1);
        }
        else
        {
            character.rb.velocity = new Vector2((character.isFacingRight ? 1 : -1) * (character.dashSpeed * 0.25f) * Time.deltaTime, character.rb.velocity.y);

            if (character.rb.velocity.x < 0)
                character.rb.velocity = new Vector2(character.rb.velocity.x + Time.deltaTime, character.rb.velocity.y);
            else
                character.rb.velocity = new Vector2(character.rb.velocity.x - Time.deltaTime, character.rb.velocity.y);
            
            

            if (character.dashCounter >= character.dashTime + character.dashCooldown)
            {
                character.anim.SetBool("Dashing2", false);
                character.SetState(new PlayerLocomotion(character));
            }
                
        }
        
    }

    public override void ExitState()
    {
        character.isDashing = false;
        character.dashCounter = 0;

    }
}

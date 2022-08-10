using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : CharacterState
{
    PlayerController playerController;
    

    public PlayerLocomotion(PlayerController player) : base(player)
    {
    }
    public float nextDashTime = 0.3f;
    public float cooldownTimer = 0;
    public float dashhCooldown = 0;


    float horizontal;

    public override void Tick()
    {
        cooldownTimer += Time.deltaTime;

        if(character.keyboardInput == true)
        {
            if (character.isDashing)
                return;

            horizontal = Input.GetAxisRaw("Horizontal");

            if (character.PlayerTouchGround(Vector2.down))
                character.anim.SetFloat("Speed", Mathf.Abs(horizontal));
            else
                character.anim.SetFloat("Speed", 0);

            if (!character.isFacingRight && horizontal > 0)
                Flip();
            if (character.isFacingRight && horizontal < 0)
                Flip();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerJumping();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                
                if (cooldownTimer > nextDashTime)
                {
                    
                    
                    character.SetState(new PlayerDash(character));
                    
                    
                }
                
            }
            

            if (Input.GetKeyDown(KeyCode.X))
                character.SetState(new PlayerAttack(character));


            if (Input.GetKeyDown(KeyCode.Z))
            {
                
            }
        }
    }

    
        
    

    public override void PhysicTick()
    {
        if (character.keyboardInput == true)
        {
            if (!InGameTracker.instance.isPause)
            character.rb.velocity = new Vector2(horizontal * character.speed * Time.deltaTime, character.rb.velocity.y);
            else
            {
                character.rb.velocity = Vector2.zero;
                character.anim.SetFloat("Speed", 0);
            }
        }
    }

    public void Flip()
    {
        if (character.isFacingRight)
        {
            character.isFacingRight = false;
            character.transform.Rotate(0, 180f, 0);
        }
        else
        {
            character.isFacingRight = true;
            character.transform.Rotate(0, 180f, 0);
        }
    }

    public override void ExitState()
    {
        character.anim.SetFloat("Speed", Mathf.Abs(horizontal));

    }

    void PlayerJumping()
    {
        if(character.keyboardInput == true)
        {
            if (character.jumpCounter <= 0)
                return;

            character.anim.SetTrigger("Jump");
            character.jumpCounter -= 1;

            if (character.jumpCounter >= 1)
                character.rb.velocity = new Vector2(character.rb.velocity.x, character.jumpPower*0.04f);
            else if (character.jumpCounter < 1)
                character.rb.velocity = new Vector2(character.rb.velocity.x, character.jumpPower*0.04f);
        }
    }

    public void Dash()
    {
        character.SetState(new PlayerDash(character));
    }
}

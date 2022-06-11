using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : CharacterState
{
    public PlayerLocomotion(PlayerController player) : base(player)
    {
    }

    float horizontal;

    public override void Tick()
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
             character.SetState(new PlayerDash(character));

        

        if (Input.GetKeyDown(KeyCode.X))
             character.SetState(new PlayerAttack(character));


        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
    }

    public override void PhysicTick()
    {
        character.rb.velocity = new Vector2(horizontal * character.speed * Time.deltaTime, character.rb.velocity.y);
    }

    void Flip()
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
        character.anim.SetFloat("Speed", 0);
    }

    void PlayerJumping()
    {
        if (character.jumpCounter <= 0)
            return;

        character.anim.SetTrigger("Jump");
        character.jumpCounter -= 1;

        if (character.jumpCounter >= 1)
            character.rb.AddForce(Vector2.up * character.jumpPower, ForceMode2D.Force);
        else if (character.jumpCounter < 1)
            character.rb.AddForce(Vector2.up * character.jumpPower * 1.3f, ForceMode2D.Force);
    }
}

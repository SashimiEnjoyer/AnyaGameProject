using UnityEngine;

/// <summary>
/// Default State
/// </summary>
public class PlayerLocomotion : CharacterState
{

    public PlayerLocomotion(PlayerController player) : base(player)
    {
    }
    //public float nextDashTime = 0.3f;
    public float cooldownTimer = 0;
    public float dashhCooldown = 0;


    public override void EnterState()
    {
        character.SetAnimatorState(character.anim, "Anya_Idle");
    }

    public override void Tick()
    {
        character.anim.SetBool("Is Ground", character.PlayerTouchGround(Vector2.down));

        if (character.keyboardInput == true)
        {

            character.horizontalInput = Input.GetAxisRaw("Horizontal");
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                DashKeyPressed();
            }

            if (character.PlayerTouchGround(Vector2.down))
            {
                character.anim.SetFloat("Speed", Mathf.Abs(character.horizontalInput));
            }
                
            else
            {
                character.anim.SetFloat("Speed", 0);
            }   

            if (!character.isFacingRight && character.horizontalInput > 0)
                Flip();
            if (character.isFacingRight && character.horizontalInput < 0)
                Flip();


            if (Input.GetKeyDown(KeyCode.E))
            {
                character.PlayerTouchEntity(character.dialogueEntity, Vector2.right).collider.GetComponent<IInteractable>().ExecuteInteractable();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerJumping();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                character.SetState(character.playerAttackState);
            }
           
            if (Input.GetKeyDown(KeyCode.Z))
            {
                
            }
        }
           
    }

    public override void PhysicTick()
    {
        character.rb.velocity = new Vector2(character.horizontalInput * character.speed * Time.deltaTime, character.rb.velocity.y);
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
        character.anim.SetFloat("Speed", Mathf.Abs(character.horizontalInput));
    }

    void PlayerJumping()
    {

        Vector3 vel = character.rb.velocity;

        if(character.keyboardInput == true)
        {
            if (character.jumpCounter <= 0)
                return;

            character.jumpCounter -= 1;
            vel.y += Mathf.Sqrt(-2f * Physics.gravity.y * character.jumpPower);

            character.rb.velocity = vel;
        }
    }

    public void Dash()
    {
        character.SetState(character.playerDashState);
    }


    void AttackKeyPressed()
    {

    }

    void JumpKeyPressed()
    {

    }

    void DashKeyPressed()
    {
        if (character.CanDash())
        {
            character.SetState(character.playerDashState);

        }
    }
}

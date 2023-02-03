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
        Debug.Log("Locomotion State!");
        InGameInput.instance.onDashPressed += DashKeyPressed;
        InGameInput.instance.onJumpPressed += PlayerJumping;
        InGameInput.instance.onAttackPressed += AttackKeyPressed;
        InGameInput.instance.onInteractPressed += InteractKeyPressed;
        character.SetAnimatorState(character.anim, "Anya_Idle");
    }

    public override void ExitState()
    {
        InGameInput.instance.onDashPressed -= DashKeyPressed;
        InGameInput.instance.onJumpPressed -= PlayerJumping;
        InGameInput.instance.onAttackPressed -= AttackKeyPressed;
        InGameInput.instance.onInteractPressed -= InteractKeyPressed;
        character.anim.SetFloat("Speed", Mathf.Abs(character.horizontalInput));
    }

    public override void Tick()
    {
        character.anim.SetBool("Is Ground", character.PlayerTouchGround(Vector2.down));

        if (character.PlayerTouchGround(Vector2.down))
        {
            character.anim.SetFloat("Speed", Mathf.Abs(character.horizontalInput));
        }

        else
        {
            character.anim.SetFloat("Speed", 0);
        }

        if (!character.isFacingRight && character.horizontalInput > 0)
            character.Flip();
        if (character.isFacingRight && character.horizontalInput < 0)
            character.Flip();

    }

    public override void PhysicTick()
    {
        character.rb.velocity = new Vector2(character.horizontalInput * character.speed * Time.deltaTime, character.rb.velocity.y);
    }

    void PlayerJumping()
    {

        if (character.jumpCounter <= 0)
            return;

        character.rb.velocity = new Vector2(character.rb.velocity.x, 0);

        Vector3 vel = character.rb.velocity;

        vel.y += Mathf.Sqrt(-2f * -10f * character.jumpPower);

        character.rb.velocity = vel;
        
        character.jumpCounter -= 1;
        
    }


    void AttackKeyPressed()
    {
        character.SetState(character.playerAttackState);
    }

    void DashKeyPressed()
    {
        if (character.CanDash())
        {
            character.SetState(character.playerDashState);

        }
    }

    void InteractKeyPressed()
    {
        character.PlayerTouchEntity(character.dialogueEntity, Vector2.right).collider.GetComponent<IInteractable>().ExecuteInteractable();
    }

}

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

    bool clingWall = false;

    public override void EnterState()
    {
        GameManager.instance.Input.onSkill1Pressed += Skill1Pressed;
        GameManager.instance.Input.onSkill2Pressed += Skill2Pressed;
        GameManager.instance.Input.onSkill3Pressed += Skill3Pressed;
        GameManager.instance.Input.onJumpPressed += PlayerJumping;
        GameManager.instance.Input.onAttackPressed += AttackKeyPressed;
        GameManager.instance.Input.onInteractPressed += InteractKeyPressed;
        character.SetAnimatorState(character.anim, "Anya_Idle");

        clingWall = false;
    }

    public override void ExitState()
    {
        GameManager.instance.Input.onSkill1Pressed -= Skill1Pressed;
        GameManager.instance.Input.onSkill2Pressed -= Skill2Pressed;
        GameManager.instance.Input.onSkill3Pressed -= Skill3Pressed;
        GameManager.instance.Input.onJumpPressed -= PlayerJumping;
        GameManager.instance.Input.onAttackPressed -= AttackKeyPressed;
        GameManager.instance.Input.onInteractPressed -= InteractKeyPressed;
        character.anim.SetFloat("Speed", Mathf.Abs(character.horizontalInput));
    }

    public override void Tick()
    {
        CheckWall();

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

    void CheckWall()
    {
        Vector2 direction = character.isFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D wallHit = Physics2D.Raycast(character.transform.position, direction, 1f, character.wallLayer);

        clingWall = wallHit.collider != null && !character.PlayerTouchGround(Vector2.down);

        if (clingWall)
        {
            character.SetState(character.playerWallHangingState);
        }
    }

    public override void PhysicTick()
    {
        if(!clingWall)
            character.rb.linearVelocity = new Vector2(character.horizontalInput * character.speed * Time.deltaTime, character.rb.linearVelocity.y);
    }

    void PlayerJumping()
    {
        character.jumpCounter -= 1;

        if (character.jumpCounter <= 0)
            return;

        character.PlayerJumping(character.rb.linearVelocityX);
    }

    void AttackKeyPressed()
    {
        character.SetState(character.playerAttackState);
    }

    void Skill1Pressed()
    {
        character.SetState(LevelManager.instance.SkillManager.GetSkill(0, this));   
    }

    void Skill2Pressed()
    {
        //character.SetState(character.skillManager.GetSkill(1, this));
    }

    void Skill3Pressed()
    {
        //character.SetState(character.skillManager.GetSkill(2, this));
    }

    void InteractKeyPressed()
    {
        character.PlayerTouchEntity(character.dialogueEntity, Vector2.right).collider?.GetComponent<IInteractable>().ExecuteInteractable();
    }

}

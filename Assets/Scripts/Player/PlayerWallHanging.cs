using UnityEngine;

public class PlayerWallHanging : CharacterState
{
    public PlayerWallHanging(PlayerController player) : base(player)
    {
    }

    bool beginCountdown = false;
    bool clingWall = false;
    private float endOfStateTiming = 0f;

    public override void EnterState()
    {
        InGameInput.instance.onJumpPressed += PlayerJumping;
        character.horizontalInput = 0;
        

        character.jumpCounter = 3;
    }

    public override void ExitState()
    {
        InGameInput.instance.onJumpPressed -= PlayerJumping;
        PlayerStats.instance.startingStats.gravityForce = 0.03f;
        beginCountdown = false;
    }

    public override void Tick()
    {
        if(beginCountdown && Time.time > endOfStateTiming)
            character.SetState(character.playerLocomotionState);

        if(character.PlayerTouchGround(Vector2.down))
            character.SetState(character.playerLocomotionState);

        CheckWall();

        if(clingWall)
            PlayerStats.instance.startingStats.gravityForce = -0.1f;
        else
            PlayerStats.instance.startingStats.gravityForce = 0.03f;

    }

    void CheckWall()
    {
        Vector2 direction = character.isFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D wallHit = Physics2D.Raycast(character.transform.position, direction, 1f, character.groundLayer);

        clingWall = wallHit.collider != null && !character.PlayerTouchGround(Vector2.down);

        if (clingWall)
        {
            character.SetState(character.playerWallHangingState);
        }
    }

    void PlayerJumping()
    {
        character.jumpCounter -= 1;

        if (character.jumpCounter <= 0)
            return;

        float jumpDir;

        if (character.jumpCounter <= 1)
        {
            jumpDir = character.horizontalInput * 15;

            if (!character.isFacingRight && character.horizontalInput > 0)
            {
                character.Flip();
            }
            if (character.isFacingRight && character.horizontalInput < 0)
            {
                character.Flip();
            }
        }
        else
        {
            jumpDir = (character.isFacingRight ? -15 : 15);
            character.Flip();
        }

        character.PlayerJumping(jumpDir);

        endOfStateTiming = Time.time + 0.7f;

        beginCountdown = true;

    }
}

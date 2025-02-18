using UnityEngine;

public class PlayerWallHanging : CharacterState
{
    public PlayerWallHanging(PlayerController player) : base(player)
    {
    }

    bool beginCountdown = false;
    private float endOfStateTiming = 0f;

    public override void EnterState()
    {
        InGameInput.instance.onJumpPressed += PlayerJumping;
        character.horizontalInput = 0;
        character.gravityMultiplier = -0.1f;

        character.jumpCounter = 3;
    }

    public override void ExitState()
    {
        InGameInput.instance.onJumpPressed -= PlayerJumping;
        character.gravityMultiplier = 0.2f;
        beginCountdown = false;
    }

    public override void Tick()
    {
        if(beginCountdown && Time.time > endOfStateTiming)
            character.SetState(character.playerLocomotionState);

        if(character.PlayerTouchGround(Vector2.down))
            character.SetState(character.playerLocomotionState);
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

        endOfStateTiming = Time.time + 1f;

        beginCountdown = true;

    }
}

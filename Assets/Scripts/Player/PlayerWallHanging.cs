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
    }

    public override void ExitState()
    {
        InGameInput.instance.onJumpPressed -= PlayerJumping;
        character.gravityMultiplier = 0.2f;
        character.horizontalInput = 0;
        beginCountdown = false;
    }

    public override void Tick()
    {
        if(beginCountdown && Time.time > endOfStateTiming)
            character.SetState(character.playerLocomotionState);

    }

    void PlayerJumping()
    {
        character.jumpCounter -= 1;

        if (character.jumpCounter <= 0)
            return;

        float jumpDir;

        jumpDir = (character.isFacingRight ? -10 : 10);
        character.horizontalInput = character.isFacingRight ? -1 : 1;
        character.Flip();

        Vector3 vel = character.rb.linearVelocity;

        vel.y += Mathf.Sqrt(-2f * -10f * character.jumpPower);

        character.rb.linearVelocity = new Vector2(jumpDir, vel.y);
        
        endOfStateTiming = Time.time + 0.5f;

        beginCountdown = true;

    }
}

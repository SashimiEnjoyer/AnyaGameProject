using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : CharacterState
{
    public PlayerDash(PlayerController player) : base(player)
    {
    }

    float dashCounter;

    public override void EnterState()
    {
        _player.isDashing = true;
    }

    public override void PhysicTick()
    {
        dashCounter += Time.deltaTime;

        if (dashCounter < _player.dashTime)
        {
            _player.rb.velocity = new Vector2((_player.isFacingRight ? 1 : -1) * _player.dashSpeed * Time.deltaTime, _player.rb.velocity.y);
        }
        else
        {
            _player.rb.velocity = new Vector2(0, _player.rb.velocity.y);

            if (dashCounter >= _player.dashTime + _player.dashCooldown)
            {
                dashCounter = 0;
                _player.SetState(new PlayerLocomotion(_player));
            }
        }
    }

    public override void ExitState()
    {
        _player.isDashing = false;
    }
}

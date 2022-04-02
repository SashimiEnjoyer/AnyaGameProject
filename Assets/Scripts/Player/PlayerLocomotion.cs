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
        if (_player.isDashing)
            return;

        horizontal = Input.GetAxisRaw("Horizontal");

        if (_player.PlayerTouchGround(Vector2.down))
            _player.anim.SetFloat("Speed", Mathf.Abs(horizontal));
        else
            _player.anim.SetFloat("Speed", 0);

        if (!_player.isFacingRight && horizontal > 0)
            Flip();
        if (_player.isFacingRight && horizontal < 0)
            Flip();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJumping();
        }

        if (Input.GetKeyDown(KeyCode.C))
             _player.SetState(new PlayerDash(_player));

        

        if (Input.GetKeyDown(KeyCode.X))
             _player.SetState(new PlayerAttack(_player));


        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
    }

    public override void PhysicTick()
    {
        _player.rb.velocity = new Vector2(horizontal * _player.speed * Time.deltaTime, _player.rb.velocity.y);
    }

    void Flip()
    {
        if (_player.isFacingRight)
        {
            _player.isFacingRight = false;
            _player.transform.Rotate(0, 180f, 0);
        }
        else
        {
            _player.isFacingRight = true;
            _player.transform.Rotate(0, 180f, 0);
        }
    }

    public override void ExitState()
    {
        _player.anim.SetFloat("Speed", 0);
    }

    void PlayerJumping()
    {
        if (_player.jumpCounter <= 0)
            return;

        _player.anim.SetTrigger("Jump");
        _player.jumpCounter -= 1;

        if (_player.jumpCounter >= 1)
            _player.rb.AddForce(Vector2.up * _player.jumpPower, ForceMode2D.Force);
        else if (_player.jumpCounter < 1)
            _player.rb.AddForce(Vector2.up * _player.jumpPower * 1.3f, ForceMode2D.Force);
    }
}

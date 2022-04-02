using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : CharacterState
{
    public PlayerAttack(PlayerController player) : base(player)
    {
    }

    float time;

    public override void EnterState()
    {
        _player.anim.SetTrigger("Attack");
        _player.attackAudio.PlayOneShot(_player.attackClip[Random.Range(0, 5)]);
    }

    public override void Tick()
    {
        time += Time.deltaTime;

        if (time < _player.AnimationLength("Anya_Attack1"))
        {
            for (int i = 0; i < _player.PlayerTouchEnemy(_player.isFacingRight).Length; i++)
            {
                if (_player.listOfEnemies.Contains(_player.PlayerTouchEnemy(_player.isFacingRight)[i].collider))
                    continue;
                else
                {
                    _player.listOfEnemies.Add(_player.PlayerTouchEnemy(_player.isFacingRight)[i].collider);

                    foreach (var enemy in _player.PlayerTouchEnemy(_player.isFacingRight)[i].collider.GetComponents<IEnemy>())
                        enemy.EnemyAttacked(_player.transform.position);

                }
            }
        }
        else
        {
            time = _player.AnimationLength("Anya_Attack1");
            _player.SetState(new PlayerLocomotion(_player));
        }

                
    }

    public override void ExitState()
    {
        _player.EmptyEnemyList();
        time = 0;
    }
}

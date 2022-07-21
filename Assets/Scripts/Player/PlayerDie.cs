using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : CharacterState
{
    public PlayerDie(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        Debug.Log("Died!");
        character.anim.SetBool("Dead", true);
        character.rb.velocity = Vector2.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : CharacterState
{
    public PlayerDie(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        character.SetAnimatortate(character.anim, "Anya_hurt");
        //character.PlayAnimationDied();
    }
}

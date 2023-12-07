using UnityEngine;

public class PlayerDie : CharacterState
{
    public PlayerDie(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        character.SetAnimatorState(character.anim, "Anya_Died");
        character.rb.velocity = Vector2.zero;
        InGameTracker.instance.onLoseEnding?.Invoke();
    }
}

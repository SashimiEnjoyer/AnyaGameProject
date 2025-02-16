using UnityEngine;

public class PlayerDie : CharacterState
{
    public PlayerDie(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        character.SetAnimatorState(character.anim, "Anya_Died");
        character.rb.linearVelocity = Vector2.zero;
        character.PlayerDie();
        InGameTracker.instance.onLoseEnding?.Invoke();
    }
}

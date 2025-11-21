using UnityEngine;

public class PlayerDie : CharacterState
{
    public PlayerDie(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        character.SetAnimatorState(character.anim, "Anya_Died");
        character.rb.linearVelocity = Vector2.zero;
        character.PlayerDie();
        LevelManager.instance.SetGameState(GameplayState.Died);
        Cursor.lockState = CursorLockMode.None;
        //InGameTracker.instance.onLoseEnding?.Invoke();
    }
}

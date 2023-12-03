using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerHurt : CharacterState
{
    public PlayerHurt(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        character.SetAnimatorState(character.anim, "Anya_Hurt");
        
        character.invulnerableCount = 0;
        character.isInvulnerable = true;
        character.isGetHitByEnemy = true;

        Time.timeScale = 0.2f;
        CameraImpulseManager.instance.ActivePlayerImpulse();
    }

    public override async void Tick()
    {
        await UniTask.Delay(100);
        Time.timeScale = 1;

        await UniTask.Delay(800); // Wait 1 sec
        
        character.SetState(character.playerLocomotionState);
    }

    public override void ExitState()
    {
        character.isGetHitByEnemy = false;
        character.WaitForInvulnerability();
    }
}


using UnityEngine;

public class PlayerHurt : CharacterState
{
    public PlayerHurt(PlayerController player) : base(player) { }

    private float endOfStateTiming = 0f;
    private float endOfSlowmoTiming = 0f;
    private bool isNormal = false;

    public override void EnterState()
    {
        character.SetAnimatorState(character.anim, "Anya_Hurt");
        
        character.invulnerableCount = 0;
        character.isInvulnerable = true;
        character.isGetHitByEnemy = true;

        endOfStateTiming = Time.time + 1f;
        endOfSlowmoTiming = Time.time + 0.12f;

        Time.timeScale = 0.5f;
        isNormal = false;
        
        GameManager.instance.CameraImpulseManager.ActivePlayerImpulse();
    }

    public override void Tick()
    {
        if (Time.time > endOfSlowmoTiming && !isNormal)
        {
            Time.timeScale = 1;
            isNormal = true;
        }

        if (Time.time > endOfStateTiming)
        {
            if(PlayerStats.instance.currentHealth > 0)
                character.SetState(character.playerLocomotionState);
            else
                character.SetState(character.playerDieState);
        }
    }

    public override void ExitState()
    {
        character.isGetHitByEnemy = false;
        character.WaitForInvulnerability();
    }
}

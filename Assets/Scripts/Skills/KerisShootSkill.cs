using UnityEngine;

public class KerisShootSkill : CharacterState
{
    private SkillStats stats;

    public KerisShootSkill(PlayerController character) : base(character)
    {
    }

    public void AssignStats(SkillStats _stats)
    {
        stats = _stats;
    }

    public override void EnterState()
    {
        GameObject go = Object.Instantiate(stats.hitbox) as GameObject;
        character.SetState(character.playerLocomotionState);
    }

    public override void ExitState()
    {
        //character.DashCooldown = true;
        stats.onSkillEnded?.Invoke();
        Debug.Log("Exit Dash State ");

        //character.nextDashTiming = Time.time + character.dashCooldown;

    }
}

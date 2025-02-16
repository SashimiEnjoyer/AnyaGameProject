using UnityEngine;

public class DashSkill : CharacterState
{
    private SkillStats stats;
    private float maxTimer;
    private float timeToMoveState;
    //private GameObject hitboxGO;

    public DashSkill(PlayerController character) : base(character)
    {
    }

    public void AssignStats(SkillStats _stats)
    {
        stats = _stats;
    }

    public override void EnterState()
    {
        character.SetAnimatorState(character.anim, "Anya_Dash");
        maxTimer = Time.time + stats.duration;
        timeToMoveState = maxTimer + 0.25f;
        character.isInvulnerable = true;

        GameObject go = Object.Instantiate(stats.hitbox, character.transform) as GameObject;
        GameObject.Destroy(go, stats.duration);
    }


    public override void PhysicTick()
    {
        
        if (Time.time < maxTimer)
        {
            character.rb.linearVelocity = new Vector2((character.isFacingRight ? 1 : -1) * stats.horizontalSpeed * Time.deltaTime, 1);
        }
        else
        {
            character.rb.linearVelocity = new Vector2((character.isFacingRight ? 1 : -1) * (stats.horizontalSpeed * 0.25f) * Time.deltaTime, character.rb.linearVelocity.y);

            if (character.rb.linearVelocity.x < 0)
                character.rb.linearVelocity = new Vector2(character.rb.linearVelocity.x + Time.deltaTime, character.rb.linearVelocity.y);
            else
                character.rb.linearVelocity = new Vector2(character.rb.linearVelocity.x - Time.deltaTime, character.rb.linearVelocity.y);

            if (Time.time >= timeToMoveState)
            {
                character.SetState(character.playerLocomotionState);
            }
                
        }
        
    }

    public override void ExitState()
    {
        //character.DashCooldown = true;
        character.isInvulnerable = false;
        stats.onSkillEnded?.Invoke();
        Debug.Log("Exit Dash State ");
        
        //character.nextDashTiming = Time.time + character.dashCooldown;

    }
}

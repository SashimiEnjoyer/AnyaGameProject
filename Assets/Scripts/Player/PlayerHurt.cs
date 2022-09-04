using Cysharp.Threading.Tasks;
public class PlayerHurt : CharacterState
{
    public PlayerHurt(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        character.SetAnimatorState(character.anim, "Anya_Hurt");
        
        character.invulnerableCount = 0;
        character.isInvulnerable = true;
        character.isGetHitByEnemy = true;
        
    }

    public override async void Tick()
    {
        await UniTask.Delay(1000); // Wait 1 sec
        
        character.SetState(character.playerLocomotionState);    
    }

    public override void ExitState()
    {
        character.isGetHitByEnemy = false;
        character.WaitForInvulnerability();
    }
}

using Cysharp.Threading.Tasks;
public class PlayerHurt : CharacterState
{
    public PlayerHurt(PlayerController player) : base(player) { }
    float timer = 0;

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
        
        character.SetState(new PlayerLocomotion(character));    
    }

    public override void ExitState()
    {
        if (PlayerStats.instance.playerHealth > 0)
            character.isGetHitByEnemy = false;
    }
}

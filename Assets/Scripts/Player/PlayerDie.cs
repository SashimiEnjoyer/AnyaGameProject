using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerDie : CharacterState
{
    public PlayerDie(PlayerController player) : base(player) { }

    public override void EnterState()
    {
        character.SetAnimatorState(character.anim, "Anya_Hurt");
    }

    public override void Tick()
    {
        character.rb.velocity = Vector2.zero;

        Died();
        
    }

    async void Died()
    {
        await UniTask.Delay(1000); //Wait 1 sec

        character.SetAnimatorState(character.anim, "Anya_Died");
    }
}

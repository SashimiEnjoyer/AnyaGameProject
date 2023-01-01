using UnityEngine;

public class EnemyDied : CharacterState
{
    public EnemyDied(EnemyController _character) : base(_character)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Enter Die State!");
        enemy.Died();
    }
}

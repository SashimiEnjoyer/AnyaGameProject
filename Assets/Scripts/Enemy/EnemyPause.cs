using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPause : CharacterState
{
    public EnemyPause(EnemyController enemy) : base(enemy) { }

    public override void EnterState()
    {
        Debug.LogWarning($"pause");
        baseEnemy.rb.linearVelocity = Vector2.zero;    
    }
}

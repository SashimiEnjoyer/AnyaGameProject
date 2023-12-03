using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPause : CharacterState
{
    public EnemyPause(EnemyController enemy) : base(enemy) { }

    public override void EnterState()
    {
        Debug.Log("Pause!!!!!!!!!!!!!");
        baseEnemy.rb.velocity = Vector2.zero;    
    }


}

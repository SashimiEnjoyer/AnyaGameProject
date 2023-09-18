using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IEnemy
{
    public void EnemyHurted()
    {
        Debug.Log("Hit!");
    }
}

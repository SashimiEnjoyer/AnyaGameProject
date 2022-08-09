using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] EnemyController enemyController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && enemyController.CanAttack == true)
        {
            collision.GetComponent<PlayerController>().PlayerHurt(transform.position, damage);
            
        }
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy
{
    public float health = 100;
    Rigidbody2D rb;

    [SerializeField] GameObject afterHitEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void EnemyAttacked(Vector2 _target)
    {
        rb.AddForce(new Vector2 (_target.x > transform.position.x ? -75 : 75, 50));
        health -= 10;

        if (health <= 0)
            Destroy(this.gameObject, 1f);
        
        if(afterHitEffect == null)
        {
            Debug.LogWarning("No Effect Prefabs Assigned");
            return;
        }

        GameObject go = Instantiate(afterHitEffect, transform.position, Quaternion.identity);
        Destroy(go, 3f);
    }
}

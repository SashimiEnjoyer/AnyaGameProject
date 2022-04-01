using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { PatrolOnly, ChasePlayer, Projectile}
public class EnemyController : MonoBehaviour, IEnemy
{
    public float health = 100;
    Rigidbody2D rb;
    bool getHit = false;
    [SerializeField] bool isFacingRight = true;
    [SerializeField] EnemyType enemyType;
    [SerializeField] GameObject afterHitEffect;
    //[SerializeField] Transform[] border = new Transform[2];

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        SwitchEnemyType(enemyType);
    }

    void SwitchEnemyType(EnemyType type)
    {
        if (getHit)
        {
            //rb.velocity = Vector2.zero;
            return;
        }

        switch (type)
        {
            case EnemyType.PatrolOnly :
                rb.velocity = new Vector2(isFacingRight ? 5 : -5, rb.velocity.y);
            break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            Flip();
        }
    }

    public void EnemyAttacked(Vector2 _target)
    {
        getHit = true;
        rb.AddForce(new Vector2 (_target.x > transform.position.x ? -250 : 250, 100));
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

    void Flip()
    {
        if (isFacingRight)
        {
            isFacingRight = false;
            transform.Rotate(0, 180f, 0);
        }
        else
        {
            isFacingRight = true;
            transform.Rotate(0, 180f, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemy
{
    public float health = 100;
    public float movementSpeed = 3f;
    public Rigidbody2D rb;
    public bool getHit = false;
    public bool isFacingRight = true;
    public GameObject afterHitEffect;
    public Transform leftBorder;
    public Transform righttBorder;
    public LayerMask playerMask;

    public CapsuleCollider2D enemyCollider;
    CharacterState currState;

    float timer;

    public void SetState(CharacterState state)
    {
        currState?.ExitState();
        currState = state;
        currState?.EnterState();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
        
    }

    private void Update()
    {
        currState.Tick();
    }

    private void FixedUpdate()
    {
        currState.PhysicTick();   
    }


    public void EnemyHurted(Vector2 _target)
    {
        getHit = true;
        rb.AddForce(new Vector2 (_target.x > transform.position.x ? -350 : 350, 100));
        health -= 10;

        if (health <= 0)
            Destroy(transform.parent.gameObject, 1f);
        
        if(afterHitEffect == null)
        {
            Debug.LogWarning("No Effect Prefabs Assigned");
            return;
        }

        GameObject go = Instantiate(afterHitEffect, transform.position, Quaternion.identity);
        Destroy(go, 3f);
    }

    public virtual void EnemyAttacking() { } 

    public void move()
    {
        if(rb != null)
            rb.velocity = new Vector2(isFacingRight ? movementSpeed : -movementSpeed, rb.velocity.y);
    }

    public void Flip()
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

    public RaycastHit2D player()
    {
        return EnemyAttackExtension.EnemyTouchPlayer(enemyCollider, playerMask, isFacingRight);
    }

   
}

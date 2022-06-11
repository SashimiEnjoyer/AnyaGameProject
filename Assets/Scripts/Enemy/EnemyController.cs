using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { PatrolOnly, Projectile, ChasePlayer, ChaseAttack}
public class EnemyController : MonoBehaviour, IEnemy
{
    public float health = 100;
    public float movementSpeed = 3f;
    public Rigidbody2D rb;
    public bool getHit = false;
    public bool isFacingRight = true;
    public EnemyType enemyType;
    public GameObject afterHitEffect;
    public Transform leftBorder;
    public Transform righttBorder;
    public LayerMask playerMask;

    CapsuleCollider2D boxCollider;
    CapsuleCollider2D playerCollider;
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
        boxCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {

        SwitchEnemyType(enemyType);

        EnemyAttacking();
    }


    public void EnemyAttacked(Vector2 _target)
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

    void SwitchEnemyType(EnemyType type)
    {

        if (getHit)
        {
            if (timer < 1)
                timer += Time.deltaTime;
            else
            {
                timer = 1;
                getHit = false;
                timer = 0;
            }
            return;
        }

        switch (type)
        {
            case EnemyType.PatrolOnly :
                move();
            break;
            case EnemyType.Projectile :

                break;
        }
    }

    void EnemyAttacking()
    {
        if (EnemyTouchPlayer(isFacingRight))
        {
            EnemyTouchPlayer(isFacingRight).collider.GetComponent<PlayerController>().PlayerAttacked(transform.position, 1);
        }
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

    void move()
    {
        if(rb != null)
            rb.velocity = new Vector2(isFacingRight ? movementSpeed : -movementSpeed, rb.velocity.y);
    } 

    public RaycastHit2D EnemyTouchPlayer(bool _isFacingRight)
    {
        return Physics2D.CapsuleCast(boxCollider.bounds.center, boxCollider.size, CapsuleDirection2D.Horizontal, 0, _isFacingRight ? Vector2.right : Vector2.left, 0.3f, playerMask);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border") && !getHit)
        {
            Flip();
        }
        if (other.CompareTag("Player") && !getHit && enemyType == EnemyType.PatrolOnly)
        {
            other.GetComponent<PlayerController>().PlayerAttacked(gameObject.transform.position, 1);   
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterStateManager, IEnemy
{
    public float health = 100;
    public float movementSpeed = 3f;
    public Rigidbody2D rb;
    public bool getHit = false;
    public bool isFacingRight = true;
    public bool PatrolAttack = true;
    public bool CanAttack = true;
    public GameObject afterHitEffect;
    public Transform leftBorder;
    public Transform righttBorder;
    public LayerMask playerMask;
    public Animator anim;
    public float AttackAnimLength = 0.01f;

    [SerializeField] GameObject enemyDialogue;

    public CapsuleCollider2D enemyCollider;

    float timer;
    float time;
    bool isStop = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        
    }

    protected override void Update()
    {
        if (isStop)
            return;

        move();
        time += Time.deltaTime;
        anim.SetBool("EnemyHurt", false);

        if (player() && !getHit && CanAttack == true)
        {
            player().collider.GetComponent<PlayerController>().PlayerHurt(transform.position, 1);
            time += Time.deltaTime;
            CanAttack = false;

            if (time < 0.2f)
            {
                anim.SetTrigger("EnemyAttack2");
            }    
        }

        if (time > AttackAnimLength)
            {
                time = 0;
                CanAttack = true;
            }

        if (movementSpeed > 0)
            anim.SetFloat("EnemySpeed", movementSpeed);
        if (movementSpeed <= 0)
            anim.SetFloat("EnemySpeed", 0);
        
    }

    public void EnemyHurted(Vector2 _target)
    {
        getHit = true;
        rb.AddForce(new Vector2 (_target.x > transform.position.x ? -350 : 350, 100));
        health -= 1;
        anim.SetTrigger("EnemyHurt2");

        if (health <= 0)
            Destroy(transform.parent.gameObject, 1f);
        
        if(afterHitEffect == null)
        {
            Debug.LogWarning("No Effect Prefabs Assigned");
            return;
        }

        GameObject go = Instantiate(afterHitEffect, transform.position, Quaternion.identity);
 
    }

    public virtual void EnemyAttacking() { } 

    public void move()
    {
        if (isFacingRight)
        {
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }

        else
        {
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }

    }

    public void Flip()
    {
        if (isFacingRight)
        {
            isFacingRight = false;
            transform.Rotate(0, 180f, 0);
            enemyDialogue.transform.Rotate(0, 180f, 0);
        }
        else
        {
            isFacingRight = true;
            transform.Rotate(0, 180f, 0);
            enemyDialogue.transform.Rotate(0, 180f, 0);
        }
    }

    public RaycastHit2D player()
    {
        return EnemyAttackExtension.EnemyTouchPlayer(enemyCollider, playerMask, isFacingRight);
    }
}

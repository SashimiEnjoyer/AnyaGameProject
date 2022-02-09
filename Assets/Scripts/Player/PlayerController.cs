using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyData
{
    public Collider2D enemy;
    public bool isAttacked;
}

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerStats playerStats;

    [Header("Run Setting")]
    [SerializeField] float speed;
    [SerializeField] bool isDashing = false;
    [SerializeField] float dashSpeed = 50f;
    [SerializeField] float dashTime = 0.5f;
    [SerializeField] float dashCooldown = 0.5f;
    float dash = 0f;
    bool isFacingRight = true;

    [Header("Jump Setting")]
    [SerializeField] float jumpPower;
    //[SerializeField] bool isJump = false;
    [SerializeField] int jumpCounter = 2;

    [Header("Define Interactable Entity")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask dialogueEntity;
    [SerializeField] LayerMask enemyEntity;
    [SerializeField] LayerMask movingPlatform;
    [SerializeField] float radiusDetection = 0.5f;

    [Header("Audio Setting")]
    [SerializeField] AudioSource movementAudio;
    [SerializeField] AudioSource attackAudio;
    [SerializeField] AudioClip[] attackClip;

    Rigidbody2D rb;
    float horizontal;
    CircleCollider2D playerCollider;
    bool isAttacking = false;
    List<Collider2D> listOfEnemies = new List<Collider2D>();


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (playerStats.playerIsDie)
            return;

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJumping();
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (PlayerTouchEntity(dialogueEntity, Vector2.right))
                PlayerTouchEntity(dialogueEntity, Vector2.right).collider.GetComponent<IDialogue>().ExecuteDialogue();        
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if(!isDashing)
                isDashing = true;
        }

        if (Input.GetKey(KeyCode.X))
        {
            attackAudio.PlayOneShot(attackClip[Random.Range(0, 5)]);
            for (int i = 0; i < PlayerTouchEnemy(isFacingRight).Length; i++)
            {
                if (listOfEnemies.Contains(PlayerTouchEnemy(isFacingRight)[i].collider))
                    continue;
                else
                {
                    listOfEnemies.Add(PlayerTouchEnemy(isFacingRight)[i].collider);

                    foreach (var enemy in PlayerTouchEnemy(isFacingRight)[i].collider.GetComponents<IEnemy>())
                        enemy.EnemyAttacked(transform.position);
                            
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }

        if (!isFacingRight && horizontal > 0)
            Flip();
        if (isFacingRight && horizontal < 0)
            Flip();

        if (PlayerTouchEntity(groundLayer, Vector2.down) && jumpCounter <= 0)
            jumpCounter = 2;

        if (PlayerTouchEntity(movingPlatform, Vector2.down))
            transform.SetParent(PlayerTouchEntity(movingPlatform, Vector2.down).transform);
        else
            transform.SetParent(null);

    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);

        }
        else
        {
            //if (Mathf.Abs(horizontal) <= 0.1f)
            //{
            //    isDashing = false;
            //    return;
            //}

            dash += Time.deltaTime;

            if (dash < dashTime)
            {
                rb.velocity = new Vector2((isFacingRight ? 1 : -1) * dashSpeed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);

                if (dash >= dashTime + dashCooldown)
                {
                    isDashing = false;
                    dash = 0;
                }
            }
        }

    }

    void PlayerJumping()
    {
        if (jumpCounter <= 0)
            return;

        jumpCounter -= 1;
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    RaycastHit2D PlayerTouchEntity(LayerMask _entityLayer, Vector2 _detectionDirection)
    {
        return Physics2D.CircleCast(playerCollider.bounds.center, playerCollider.radius, _detectionDirection, radiusDetection, _entityLayer);
    }

    RaycastHit2D[] PlayerTouchEnemy(bool _isFacingRight)
    {
        return Physics2D.CircleCastAll(playerCollider.bounds.center, playerCollider.radius, _isFacingRight? Vector2.right : Vector2.left, radiusDetection, enemyEntity);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(playerCollider.bounds.center, playerCollider.radius + 0.5f);
    //}

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

    [ContextMenu("Remove All Enemies")]
    public void EmptyEnemyList()
    {
        listOfEnemies.Clear();
    }
}

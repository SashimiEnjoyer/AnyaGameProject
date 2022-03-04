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
    public float speed;
    public bool isDashing = false;
    public float dashSpeed = 50f;
    public float dashTime = 0.5f;
    public float dashCooldown = 0.5f;
    public float dash = 0f;
    public bool isFacingRight = true;

    [Header("Jump Setting")]
    public float jumpPower;
    public int jumpCounter = 2;

    [Header("Define Interactable Entity")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask dialogueEntity;
    [SerializeField] LayerMask enemyEntity;
    [SerializeField] LayerMask movingPlatform;
    [SerializeField] float radiusDetection = 0.5f;

    [Header("Audio Setting")]
    public AudioSource movementAudio;
    public AudioSource attackAudio;
    public AudioClip[] attackClip;

    public Rigidbody2D rb;
    public bool isAttacking = false;
    public Animator anim;
    public List<Collider2D> listOfEnemies = new List<Collider2D>();

    CapsuleCollider2D playerCollider;
    CharacterState currState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }
    
    public void SetState(CharacterState state)
    {
        currState?.ExitState();
        currState = state;
        currState?.EnterState();
    }

    private void Start()
    {
        SetState(new PlayerLocomotion(this));
    }

    void Update()
    {
        if (playerStats.playerIsDie)
            return;

        currState?.Tick();

        anim.SetBool("IsJump", !PlayerTouchEntity(groundLayer, Vector2.down));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJumping();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerTouchEntity(dialogueEntity, Vector2.right))
                PlayerTouchEntity(dialogueEntity, Vector2.right).collider.GetComponent<IDialogue>().ExecuteDialogue();        
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (CanMoveState())
            {
                SetState(new PlayerDash(this));
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if(CanMoveState())
                SetState(new PlayerAttack(this));
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }

        if (PlayerTouchEntity(groundLayer, Vector2.down) && jumpCounter <= 0)
            jumpCounter = 2;

        if (PlayerTouchEntity(movingPlatform, Vector2.down))
            transform.SetParent(PlayerTouchEntity(movingPlatform, Vector2.down).transform);
        else
            transform.SetParent(null);

    }

    private void FixedUpdate()
    {
        currState?.PhysicTick();
    }

    void PlayerJumping()
    {
        if (jumpCounter <= 0)
            return;

        anim.SetTrigger("Jump");
        jumpCounter -= 1;

        if(jumpCounter >= 1)
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
        else if (jumpCounter < 1)
            rb.AddForce(Vector2.up * jumpPower * 1.3f, ForceMode2D.Force);
    }

    RaycastHit2D PlayerTouchEntity(LayerMask _entityLayer, Vector2 _detectionDirection)
    {
        return Physics2D.CapsuleCast(playerCollider.bounds.center, playerCollider.size, CapsuleDirection2D.Vertical, 0.5f, _detectionDirection, 0.5f, _entityLayer);
        //return Physics2D.CircleCast(playerCollider.bounds.center, playerCollider.radius, _detectionDirection, radiusDetection, _entityLayer);
    }

    public RaycastHit2D PlayerTouchGround(Vector2 _detectionDirection)
    {
        //return Physics2D.CircleCast(playerCollider.bounds.center, playerCollider.radius, _detectionDirection, radiusDetection, groundLayer);
        return Physics2D.CapsuleCast(playerCollider.bounds.center, playerCollider.size,CapsuleDirection2D.Vertical, 0.5f, _detectionDirection, 0.5f, groundLayer);
    }

    public RaycastHit2D[] PlayerTouchEnemy(bool _isFacingRight)
    {
        return Physics2D.CapsuleCastAll(playerCollider.bounds.center, playerCollider.size, CapsuleDirection2D.Vertical, 0, _isFacingRight ? Vector2.right : Vector2.left, 0.5f, enemyEntity);
        //return Physics2D.CircleCastAll(playerCollider.bounds.center, playerCollider.radius, _isFacingRight? Vector2.right : Vector2.left, radiusDetection, enemyEntity);
    }

    bool CanMoveState()
    {
        return !isDashing && !isAttacking;
    }

    //bool CanDash()
    //{
    //    return !isDashing && !isAttacking;
    //}

    //bool CanAttack()
    //{
    //    return !isDashing && !isAttacking;
    //}
  
    [ContextMenu("Remove All Enemies")]
    public void EmptyEnemyList()
    {
        listOfEnemies.Clear();
    }

    public float AnimationLength(string animationName)
    {
        float time = 0;
        RuntimeAnimatorController ra = anim.runtimeAnimatorController;

        for (int i = 0; i < ra.animationClips.Length; i++)
        {
            if(ra.animationClips[i].name == animationName)
            {
                time = ra.animationClips[i].length;
            }
        }
        return time;
    }

    //private void OnDrawGizmos()
    //{
        
    //    //izmos.DrawWireSphere(playerCollider.bounds.center, playerCollider.radius + 0.5f);
    //}

}

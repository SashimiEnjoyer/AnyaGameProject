using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct EnemyData
{
    public Collider2D enemy;
    public bool isAttacked;
}

public class PlayerController : CharacterStateManager
{
    public Action OnDashKeyPressed;
    public Action OnJumpKeyPressed;
    public Action OnAttackKeyPressed;

    [Header("Run Setting")]
    public float speed;
    public float dashSpeed = 50f;
    public float dashCounter = 0f;
    public float dashTime = 0.5f;
    public float dashCooldown = 10;
    public float dash = 0f;
    
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

    [Header("Conditions")]
    public bool isDashing = false;
    public bool isFacingRight = true;
    public bool isGetHitByEnemy = false;
    public bool isInvulnerable = false;
    public bool isDead = false;
    public bool isAttacking = false;
    public bool isJumping = false;

    [Header("Effect")]
    [SerializeField] HitEffect hitEffect;
    
    public Rigidbody2D rb;
    public Animator anim;
    public List<Collider2D> listOfEnemies = new List<Collider2D>();

    CapsuleCollider2D playerCollider;
    public float invulnerableCount = 0;
    [SerializeField] bool isStop = false;

    private float commontime;
    private Vector3 SpawnPos;
    public bool keyboardInput = true;
    private PlayerAnimations playerAnimations;

    private float commontimejumping;
    private float commontimeplatfrom;
    [SerializeField] private LayerMask layer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        SpawnPos = transform.position;
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void OnDestroy()
    {
        InGameTracker.instance.onGameStateChange -= SwitchGameState;
    }

    private void Start()
    {
        InGameTracker.instance.onGameStateChange += SwitchGameState;
        SetState(new PlayerLocomotion(this));
    }

    protected override void Update()
    {
        commontimeplatfrom += Time.deltaTime;
        if (commontimeplatfrom >= 0.5f)
        {
            commontimeplatfrom = 0;
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        
        
        if (isStop)
            return;

        base.Update();

        if (isJumping == true)
        {
            commontimejumping += Time.deltaTime;

            if (commontimejumping >= AnimationLength("Anya_JumpUp"))
            {
                isJumping = false;
                commontimejumping = 0;
            }
        }


        if (!PlayerTouchGround2(Vector2.down) && isAttacking == false && isJumping == false && isDashing == false && isDead == false && isGetHitByEnemy == false)
        playerAnimations.PlayAnimationJumpGround();

        if (Input.GetKeyDown(KeyCode.E))
        {
            //if (PlayerTouchEntity(dialogueEntity, Vector2.right))
            PlayerTouchEntity(dialogueEntity, Vector2.right).collider.GetComponent<IInteractable>().ExecuteInteractable();        
            
        }
        
        if (PlayerStats.instance.playerHealth <= 0)
        {
            commontime += Time.deltaTime;
            isDead = true;

            if (commontime >= AnimationLength("Anya_Hurt"))
            {
                transform.position = SpawnPos;
                SetState(new PlayerLocomotion(this));
                PlayerStats.instance.playerHealth = 3;
                isDead = false;
                commontime = 0;
            }
        }
        
        // Manual Level Reset.
        if (Input.GetKeyDown(KeyCode.T))
        {
            ResetGame();
        }

        if (PlayerTouchGround(Vector2.down))
        {
            jumpCounter = 2;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            commontimeplatfrom = 0;
            gameObject.layer = LayerMask.NameToLayer("Player Platform Fall");
        }

           
        if (PlayerTouchEntity(movingPlatform, Vector2.down))
            transform.SetParent(PlayerTouchEntity(movingPlatform, Vector2.down).transform);
        else
            transform.SetParent(null);

        if (isInvulnerable)
        {
            if(invulnerableCount < AnimationLength("Anya_Hurt"))
            {
                invulnerableCount += Time.deltaTime;
            }else
            {
                invulnerableCount = 2;
                isInvulnerable = false;
            }
        }

    }

    protected override void FixedUpdate()
    {
        if(isStop)
        {
            rb.velocity = Vector2.zero;
            anim.SetFloat("Speed", rb.velocity.x);
            return;
        }

        base.FixedUpdate();
    }


    public void SwitchGameState(GameplayState state)
    {
        switch (state)
        {
            case GameplayState.Pause:
            case GameplayState.Dialogue:
                isStop = true;
                SetState(new PlayerLocomotion(this));
                break;
            case GameplayState.Stop:
                isStop = true;
                if(PlayerStats.instance.playerHealth > 0)
                    SetState(new PlayerLocomotion(this));
                break; 
            case GameplayState.Playing:
                isStop = false;
                break;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    RaycastHit2D PlayerTouchEntity(LayerMask _entityLayer, Vector2 _detectionDirection)
    {
        return Physics2D.CapsuleCast(playerCollider.bounds.center, playerCollider.size, CapsuleDirection2D.Horizontal, 0.5f, _detectionDirection, 0.5f, _entityLayer);
    }

    public RaycastHit2D PlayerTouchGround(Vector2 _detectionDirection)
    {
        return Physics2D.CapsuleCast(playerCollider.bounds.center, playerCollider.size,CapsuleDirection2D.Vertical, 0.01f, _detectionDirection, 0.01f, groundLayer);
    }

    public RaycastHit2D PlayerTouchGround2(Vector2 _detectionDirection)
    {
        return Physics2D.CapsuleCast(playerCollider.bounds.center, playerCollider.size,CapsuleDirection2D.Vertical, 0.1f, _detectionDirection, 0.1f, groundLayer);
    }

    public RaycastHit2D[] PlayerTouchEnemy(bool _isFacingRight)
    {
        return Physics2D.CapsuleCastAll(playerCollider.bounds.center, playerCollider.size, CapsuleDirection2D.Horizontal, 0, _isFacingRight ? Vector2.right : Vector2.left, 1f, enemyEntity);
    }

    public void PlayerHurt(Vector2 _target, int damage)
    {
        if (!isInvulnerable && PlayerStats.instance.playerHealth != 0)
        {

            rb.AddForce(new Vector2(_target.x > transform.position.x ? -100 : 100, 150));
            PlayerStats.instance.playerHealth -= damage;
            SetState(new PlayerHurt(this));

        }
    }

    [ContextMenu("Remove All Enemies")]
    public void EmptyEnemyList()
    {
        listOfEnemies.Clear();
    }

    public float AnimationLength(string animationName)
    {
        RuntimeAnimatorController ra = anim.runtimeAnimatorController;

        for (int i = 0; i < ra.animationClips.Length; i++)
        {
            if(ra.animationClips[i].name == animationName)
            {
                return ra.animationClips[i].length;
            }
        }
        Debug.LogError("Animation Name not found!");
        return 0;
    }

    public void Dash()
    {
        SetState(new PlayerDash(this));
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

    public void PlayAnimationAttack()
    {
        playerAnimations.PlayAnimationAttack();
    }

    public void PlayAnimationJumpUp()
    {
        playerAnimations.PlayAnimationJumpUp();
    }

    public void PlayAnimationDash()
    {
        playerAnimations.PlayAnimationDash();
    }

    public void PlayAnimationHurt()
    {
        playerAnimations.PlayAnimationHurt();
    }

    public void PlayAnimationDied()
    {
        playerAnimations.PlayAnimationDied();
    }
}

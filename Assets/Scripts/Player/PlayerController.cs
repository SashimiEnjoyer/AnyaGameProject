using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.TextCore.Text;

/// <summary>
/// Set and Get Overall Conditions for Player 
/// </summary>
public class PlayerController : CharacterStateManager
{
    public Action OnDashKeyPressed;
    public Action OnJumpKeyPressed;
    public Action OnAttackKeyPressed;

    public PlayerAttack playerAttackState;
    public PlayerDash playerDashState;
    public PlayerDie playerDieState;
    public PlayerHurt playerHurtState;
    public PlayerLocomotion playerLocomotionState;

    [Header("Run Setting")]
    public float horizontalInput = 0f;
    public float speed;
    public float dashSpeed = 50f;
    public float dashCounter = 0f;
    public float dashTime = 0.5f;
    public float dashCooldown = 10;
    public float nextDashTiming = 0f;

    [Header("Attack Setting")]
    public GameObject attackSlashEffectPrefab;
    
    [Header("Jump Setting")]
    public float jumpPower;
    public int jumpCounter = 2;

    [Header("Define Interactable Entity")]
    public LayerMask groundLayer;
    public LayerMask dialogueEntity;
    public LayerMask enemyEntity;
    public LayerMask movingPlatform;
    [SerializeField] float radiusDetection = 0.5f;

    [Header("Audio Setting")]
    public AudioSource movementAudio;
    public AudioSource attackAudio;
    public AudioClip[] attackClip;

    [Header("Conditions")]
    public bool isFacingRight = true;
    public bool isGetHitByEnemy = false;
    public bool isInvulnerable = false;
    public bool isDead = false;
    public float invulnerableCount = 0;

    [Header("Effect")]
    [SerializeField] HitEffect hitEffect;
    
    public Rigidbody2D rb;
    public Animator anim;
    public List<EnemyController> listOfEnemies = new List<EnemyController>();

    CapsuleCollider2D playerCollider;
    bool isStop = false;

    private Vector3 SpawnPos;
    public bool keyboardInput = true;

    [SerializeField] private LayerMask layer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        playerAttackState = new PlayerAttack(this);
        playerDieState = new PlayerDie(this);
        playerDashState = new PlayerDash(this);
        playerLocomotionState = new PlayerLocomotion(this);
        playerHurtState = new PlayerHurt(this);
    }

    private void OnDestroy()
    {
        InGameInput.instance.onMovePressed -= GetMoveInput;
        InGameInput.instance.onMoveStop -= GetMoveInput;
        InGameTracker.instance.onGameStateChange -= SwitchGameState;
    }

    private void Start()
    {
        InGameTracker.instance.onGameStateChange += SwitchGameState;
        InGameInput.instance.onMovePressed += GetMoveInput;
        InGameInput.instance.onMoveStop += GetMoveInput;
        SetState(playerLocomotionState);
        SpawnPos = transform.position;
    }

    protected override void Update()
    {
        
        if (isStop)
            return;

        base.Update();

        if (PlayerTouchGround(Vector2.down))
        {
            jumpCounter = 2;
        }
           
        if (PlayerTouchEntity(movingPlatform, Vector2.down))
            transform.SetParent(PlayerTouchEntity(movingPlatform, Vector2.down).transform);
        else
            transform.SetParent(null);

        if(PlayerStats.instance.playerHealth <= 0 && currState != playerDieState)
            SetState(playerDieState);

    }

    protected override void FixedUpdate()
    {
        if(isStop)
        {
            StopMove();
            anim.SetFloat("Speed", 0);
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
                
                if(currState != playerLocomotionState)
                    SetState(playerLocomotionState);
                
                isStop = true;
                break;
            case GameplayState.Stop:

                if(PlayerStats.instance.playerHealth > 0)
                    SetState(playerLocomotionState);

                isStop = true;
                break; 
            case GameplayState.Playing:

                isStop = false;
                break;
        }
    }

    public RaycastHit2D PlayerTouchEntity(LayerMask _entityLayer, Vector2 _detectionDirection)
    {
        return Physics2D.CapsuleCast(playerCollider.bounds.center, playerCollider.size, CapsuleDirection2D.Horizontal, 0.5f, _detectionDirection, 0.5f, _entityLayer);
    }

    public RaycastHit2D PlayerTouchGround(Vector2 _detectionDirection)
    {
        return Physics2D.CapsuleCast(playerCollider.bounds.center, playerCollider.size,CapsuleDirection2D.Vertical, 0.1f, _detectionDirection, 0.01f, groundLayer);
    }

    public RaycastHit2D[] PlayerTouchEnemy(bool _isFacingRight)
    {
        return Physics2D.CapsuleCastAll(playerCollider.bounds.center, playerCollider.size, CapsuleDirection2D.Horizontal, 0, _isFacingRight ? Vector2.right : Vector2.left, 1f, enemyEntity);
    }

    public void PlayerHurt(Vector2 _target, int damage)
    {
        if (!isInvulnerable && PlayerStats.instance.playerHealth > 0)
        {
            StopMove();
            rb.AddForce(new Vector2(_target.x > transform.position.x ? -100 : 100, 150));
            PlayerStats.instance.playerHealth -= damage;

            SetState(PlayerStats.instance.playerHealth > 0? playerHurtState : playerDieState);
        }
    }

    public void StopMove()
    {
        rb.velocity = Vector2.zero;
    }

    void GetMoveInput(float input)
    {
        horizontalInput = input;
    }

    public async void WaitForInvulnerability()
    {
        await UniTask.Delay(1000);
        isInvulnerable = false;
    }

    public bool CanDash()
    {
        return nextDashTiming <= Time.time;
    }

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
        SetState(playerDashState);
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
}

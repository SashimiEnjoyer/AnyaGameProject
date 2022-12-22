using UnityEngine;
using UnityEngine.Events;

public class EnemyController : CharacterStateManager, IEnemy
{
    [Header("Stats")]
    public float health = 100;
    public float movementSpeed = 3f;
    public float AttackAnimLength = 0.01f;

    [Header("Status")]
    [HideInInspector] public bool getHit = false;
    [HideInInspector] public bool isFacingRight = true;
    [HideInInspector] public bool PatrolAttack = true;
    [HideInInspector] public bool CanAttack = true;

    [Header("References")]
    public Rigidbody2D rb;
    public GameObject afterHitEffect;
    public Transform leftBorder;
    public Transform righttBorder;
    public LayerMask playerMask;
    public Animator anim;
    public CapsuleCollider2D hitBox;

    [Header("States")]
    public CharacterState patrolState;
    public CharacterState enemyHurted;

    [Header("Actions")]
    public UnityAction onTouchBorder;
    public UnityAction onEnemyDied;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hitBox = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    public virtual void EnemyHurted(Vector2 _target) { }

    public virtual void EnemyDoAttack() { }

    public virtual void move() { }
    
    public virtual void Flip() { }

    public RaycastHit2D PlayerTouched()
    {
        return EnemyAttackExtension.EnemyTouchPlayer(hitBox, playerMask, isFacingRight);
    }
}

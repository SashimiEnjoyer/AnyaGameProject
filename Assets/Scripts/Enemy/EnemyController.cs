using UnityEngine;
using UnityEngine.Events;

public enum EnemyAggroStatus
{
    Calm, Semi, Aggresive
}

public class EnemyController : CharacterStateManager, IEnemy
{
    [Header("Stats")]
    public float health = 100;
    public float movementSpeed = 3f;
    public float attackAnimLength = 0.01f;
    public float maxDistanceBeforeDie = 2f;
    public float staggerTime = 1f;
    public float aggroRadius = 2f;
    public Vector2 knockDistance;
    public bool isGround;

    [Header("Status")]
    [HideInInspector] public bool getHit = false;
    [HideInInspector] public bool isFacingRight = true;
    [HideInInspector] public bool PatrolAttack = true;
    [HideInInspector] public bool CanAttack = true;
     public bool Resetting = false;
     public int CurrentDirection = 1;
    public EnemyAggroStatus AggroStatus;

    [Header("References")]
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject afterHitEffect;
    public Transform startingPoint;
    public Transform groundChecker;
    public Transform playerTransform;
    public CapsuleCollider2D hitBox;
    public LayerMask playerMask;
    public LayerMask borderMask;

    [Header("States")]
    public CharacterState patrolState;
    public CharacterState chaseState;
    public CharacterState attackState;
    public CharacterState enemyHurted;
    public CharacterState enemyDied;

    [Header("Events")]
    public UnityEvent onEnemyDied;

    public virtual void EnemyHurted() { }

    public virtual void EnemyDoAttack() { }

    public virtual void Move(float multiplier) { }

    public virtual void StopMove() { }
    
    public virtual void Flip() { }

    public virtual void Died() { }

    public virtual void ResetPosition() { }

    public virtual void Knocked() { } 


    public RaycastHit2D CheckMask(LayerMask mask) 
    {
        return Physics2D.Raycast(groundChecker.transform.position, Vector2.right, 0.4f,mask);
    }

    public RaycastHit2D PlayerTouched()
    {
        return EnemyAttackExtension.EnemyTouchPlayer(hitBox, playerMask, isFacingRight);
    }

    public Vector2 PlayerDirection()
    {
        return (playerTransform.position - transform.position).normalized;
    }
}

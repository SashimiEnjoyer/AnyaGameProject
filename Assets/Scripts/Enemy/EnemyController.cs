using UnityEngine;
using UnityEngine.Events;

public class EnemyController : CharacterStateManager, IEnemy
{
    [Header("Stats")]
    public float health = 100;
    public float movementSpeed = 3f;
    public float attackAnimLength = 0.01f;
    public float maxDistanceBeforeDie = 2f;
    public float staggerTime = 1f;
    public Vector2 knockDistance;
    public bool isGround;

    [Header("Status")]
    [HideInInspector] public bool getHit = false;
    [HideInInspector] public bool isFacingRight = true;
    [HideInInspector] public bool PatrolAttack = true;
    [HideInInspector] public bool CanAttack = true;
    [HideInInspector] public bool Resetting = false;
    [HideInInspector] public int CurrentDirection = 1;

    [Header("References")]
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject afterHitEffect;
    public Transform startingPoint;
    public Transform groundChecker;
    public Transform player;
    public CapsuleCollider2D hitBox;
    public LayerMask playerMask;
    public LayerMask borderMask;

    [Header("States")]
    public CharacterState patrolState;
    public CharacterState enemyHurted;
    public CharacterState enemyDied;

    [Header("Actions")]
    public UnityAction onTouchBorder;
    public UnityAction onEnemyDied;

    public virtual void EnemyHurted(Vector2 _target) { }

    public virtual void EnemyDoAttack() { }

    public virtual void Move() { }

    public virtual void StopMove() { }
    
    public virtual void Flip() { }

    public virtual void Died() { }

    public virtual void Knocked() { } 


    public RaycastHit2D CheckMask(LayerMask mask) 
    {
        return Physics2D.Raycast(groundChecker.transform.position, Vector2.right, 0.4f,mask);
    }

    public RaycastHit2D PlayerTouched()
    {
        return EnemyAttackExtension.EnemyTouchPlayer(hitBox, playerMask, isFacingRight);
    }
}

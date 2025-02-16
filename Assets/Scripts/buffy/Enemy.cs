using System.Collections;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    public EnemyStateId initialState;
    public Animator animator;
    public Rigidbody2D rb;
    public LayerMask platformLayer;
    public LayerMask enemyAnchor;
    public Transform groundCheckPos;
    public Health health;

    public Collider2D bodyCollider;
    public Collider2D groundCollider;
    public Transform player;

    public float movementSpeed;
    public float jumpStrength;
    public bool facingRight = true;

    [HideInInspector]
    public Vector2 hitDirection;

    [HideInInspector]
    public EnemyStateId previousState;

    void Awake()
    {
        if (!facingRight)
        {
            Flip();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new EnemyStateMachine(this);
        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.RegisterState(new EnemyPatrol_State());
        stateMachine.RegisterState(new EnemyHitState());
        stateMachine.RegisterState(new EnemyAggroState());
        stateMachine.ChangeState(initialState);
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    public void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        movementSpeed *= -1;
    }

    public void Move()
    {
        rb.linearVelocity = new Vector2(movementSpeed * Time.fixedDeltaTime, rb.linearVelocity.y);
    }

    public void Stop()
    {
        rb.linearVelocity = new Vector2(0.0f, rb.linearVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStrength * Time.fixedDeltaTime);
        }
    }

    public void Hurt(Vector2 direction)
    {
        health.TakeDamage(1, direction);
        hitDirection = direction;
        stateMachine.ChangeState(EnemyStateId.Hit);
        StartCoroutine(HitLag());
    }

    public bool isCollideWithWall()
    {
        return bodyCollider.IsTouchingLayers(platformLayer);
    }

    public bool isCollideWithEnemyAnchor()
    {
        return bodyCollider.IsTouchingLayers(enemyAnchor);
    }

    public bool isGrounded()
    {
        return groundCollider.IsTouchingLayers(platformLayer);
    }

    public Vector2 GetPlayerPosition(Vector2 playerPos)
    {
        return player.position = playerPos;
    }

    public Vector2 getPlayerDirection()
    {
        return (player.position - transform.position).normalized;
    }

    IEnumerator HitLag()
    {
        yield return new WaitForSeconds(0.5f);
        stateMachine.ChangeState(EnemyStateId.Aggro);
    }

}

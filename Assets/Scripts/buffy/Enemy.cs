using System.Collections;
using System.Collections.Generic;
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

    public float movementSpeed;
    public bool facingRight = true;

    [HideInInspector]
    public Collider2D bodyCollider;

    void Awake() 
    {
        if(!facingRight){
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
        stateMachine.ChangeState(initialState);

        BoxCollider2D[] colliders = gameObject.GetComponentsInChildren<BoxCollider2D>();
        bodyCollider = colliders[0];
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

    public void Flip() {
        transform.localScale = new Vector2(transform.localScale.x* -1, transform.localScale.y);
        movementSpeed *= -1;
    }

    public void Move() {
        rb.velocity = new Vector2(movementSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    public void Hurt(Vector2 direction) {
        health.TakeDamage(1, direction);
        stateMachine.ChangeState(EnemyStateId.Hit);
    }

    public bool isCollideWithWall() {
        return bodyCollider.IsTouchingLayers(platformLayer);
    }

    public bool isCollideWithEnemyAnchor() {
        return bodyCollider.IsTouchingLayers(enemyAnchor);
    }

}

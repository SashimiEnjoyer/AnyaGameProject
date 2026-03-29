using Animancer;
using UnityEngine;

public class RaisinBossController : CharacterStateManager, IEnemy
{
    public Rigidbody2D rb;
    public AnimancerComponent animComponent;

    [SerializeField] private Transform player;
    [SerializeField] private int maxHP;
    [SerializeField] private int hp;
    [SerializeField] private float neutralWalkSpd;
    [SerializeField] private GameObject hitBox;
    [SerializeField] private GameObject groundPoundEffect;
    [SerializeField] private GameObject afterHitEffect;
    [SerializeField] private ClipTransition walkAnim;
    [SerializeField] private ClipTransition idleAnim;

    private int directionFacing = 1;
    public int DirectionFacing => directionFacing;
    public Vector2 neutralTimeRange;


    public EnemyBossState defaultState;
    public EnemyBossState groundPoundAtkPattern;
    public EnemyBossState dashAtkPattern;
    public EnemyBossState enemyDied;

    private void Awake()
    {
        defaultState = new BossRaisin.NeutralState(this);
        groundPoundAtkPattern = new BossRaisin.GroundPoundAttackPattern(this);
        dashAtkPattern = new BossRaisin.DashAttackPattern(this);    
        //enemyDied = new EnemyDied(this);
    }

    private void Start()
    {
        SetState(defaultState);
    }

    public void Move(float multiplier = 1f)
    {
        rb.linearVelocity = new Vector2(neutralWalkSpd * directionFacing * multiplier, rb.linearVelocity.y);
    }

    public  void StopMove()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void Flip()
    {
        directionFacing *= -1;
        StopMove();
        transform.Rotate(0, 180f, 0);
    }

    public Vector2 PlayerDirection()
    {
        return (player.position - transform.position).normalized;
    }

    public void EnemyHurted()
    {
        hp -= 1;

        ParticleSystem hitEffect = null;

        if (hitEffect == null)
        {
            hitEffect = GameObject.Instantiate(afterHitEffect, transform).GetComponent<ParticleSystem>();
            hitEffect.Play();
        }
        else
            hitEffect.Emit(50);


        if (hp <= 0)
            transform.parent.gameObject.SetActive(false);
    }
    public void InstantiateGroundPoundEffect()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject go = Instantiate(groundPoundEffect, transform);
            go.transform.parent = null;
            go.GetComponent<Rigidbody2D>().AddForceX(i % 2 != 0?  -650f : 650f);
            go.transform.localPosition = new Vector2(go.transform.localPosition.x, go.transform.localPosition.y + 2);
            go.transform.localScale = groundPoundEffect.transform.localScale;
            Destroy(go, 4f);
        }
    }

    public void PlayIdleAnim() => animComponent.Play(idleAnim);
    public void PlayWalkAnim() => animComponent.Play(walkAnim);
    public void SetActiveHitbox(bool state) => hitBox.SetActive(state);
}

using UnityEngine;

public class RaisinBossController : CharacterStateManager
{
    [SerializeField] private int maxHP;
    [SerializeField] private int hp;
    public Vector2 neutralTimeRange;


    public EnemyBossState defaultState;
    public EnemyBossState attackPattern1;
    public EnemyBossState attackPattern2;

    private void Awake()
    {
        defaultState = new BossRaisin.NeutralState(this);
        attackPattern1 = new BossRaisin.AttackPattern(this);
    }

    private void Start()
    {
        SetState(defaultState);
    }

}

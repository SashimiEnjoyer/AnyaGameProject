public abstract class CharacterState
{
    protected PlayerController character;
    protected EnemyController baseEnemy;
    protected EventController player;

    public CharacterState(PlayerController _character)
    {
        character = _character;
    }

    public CharacterState(EnemyController _enemy)
    {
        baseEnemy = _enemy;
    }

    public CharacterState(EventController _character)
    {
        player = _character;
    }

    public virtual void EnterState()
    {
        
    }

    public virtual void ExitState()
    {

    }

    public virtual void Tick()
    {
        
    }

    public virtual void PhysicTick()
    {

    }
}

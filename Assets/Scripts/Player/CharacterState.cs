public abstract class CharacterState
{
    protected PlayerController _player;

    public CharacterState(PlayerController player)
    {
        _player = player;
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

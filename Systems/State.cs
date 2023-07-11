using Terraria;
using Terraria.ModLoader;
using Annihilation.Systems;

public abstract class State
{
    protected BaseNPC baseNPC;

    public float timeInState = 0;

    public State(BaseNPC baseNPC)
    {
        this.baseNPC = baseNPC;
    }
    public virtual void OnStateEnter()
    {
        timeInState = 0;
    }

    public virtual void OnStateUpdate()
    {
        timeInState += (1f / 60f);
    }

    public virtual void OnStateExit()
    {
    }
}
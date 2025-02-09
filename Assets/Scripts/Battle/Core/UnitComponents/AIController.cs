

using System.Collections.Generic;

public enum EUnitState
{
    None = 0,
    Idle,
    Attack,
}

public class AIController : UnitComponent
{
    protected EUnitState mState = EUnitState.None;
    public EUnitState State => mState;

    protected AbilityBase mAbility = null; // 攻击技能

    public void EnterState(EUnitState state)
    {
        if (mState == state)
            return;
        
        mState = state;

        switch (mState)
        {
            case EUnitState.Idle:
                mOwner.UnitView.ModelLayout.Animator.SetTrigger("idle");
                break;
            
            case EUnitState.Attack:
                mOwner.UnitView.ModelLayout.Animator.SetTrigger("attack");
                break;
        }
    }

    public override void Start()
    {
        mAbility = mOwner.AbilityAgent.GetAbilityByType(AbilityType.AutoAttack);
    }

    public override void Update(float deltaTime)
    {
        switch (State)
        {
            case EUnitState.Idle:
                TickIdle(deltaTime);
                break;
            
            case EUnitState.Attack:
                TickAttack(deltaTime);
                break;
            
            default:
                break;
        }
    }

    protected void TickIdle(float deltaTime)
    {
        if (mAbility != null && mAbility.TryActivate())
        {
            EnterState(EUnitState.Attack);
        }
    }

    protected void TickAttack(float deltaTime)
    {
        if (mAbility == null || !mAbility.IsActivated)
        {
            EnterState(EUnitState.Idle);
        }
    }
}
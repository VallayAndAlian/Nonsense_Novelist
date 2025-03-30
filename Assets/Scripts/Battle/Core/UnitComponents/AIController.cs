

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

    protected BattleUnit mTarget = null;
    public BattleUnit Target => mTarget;

    public void EnterState(EUnitState state)
    {
        if (mState == state)
            return;
        
        mState = state;

        switch (mState)
        {
            case EUnitState.Idle:
                if (mOwner.UnitView)
                    mOwner.UnitView.ModelLayout.Animator.SetTrigger("idle1");
                break;
            
            case EUnitState.Attack:
                if (mOwner.UnitView)
                    mOwner.UnitView.ModelLayout.Animator.SetTrigger("attack1");
                break;
        }
    }

    public override void Start()
    {
        mAbility = mOwner.AbilityAgent.GetAbilityByType(AbilityType.AutoAttack);

        mState = EUnitState.Idle;
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
        if (mAbility != null)
        {
            mTarget = mAbility.PickTarget();

            if (mTarget != null)
            {
                mAbility.SetTarget(mTarget);
                
                if (mAbility.TryActivate())
                {
                    EnterState(EUnitState.Attack);
                }
            }
        }
    }

    protected void TickAttack(float deltaTime)
    {
        if (!IsValidTarget(mTarget))
        {
            if (mAbility != null)
            {
                mAbility.SetTarget(null);
                mAbility.TryDeactivate();
            }

            mTarget = null;
            
            EnterState(EUnitState.Idle);
        }
    }

    protected bool IsValidTarget(BattleUnit target)
    {
        if (mAbility is not { IsActivated: true })
            return false;

        if (mTarget is not { IsAlive: true })
            return false;

        return true;
    }
}
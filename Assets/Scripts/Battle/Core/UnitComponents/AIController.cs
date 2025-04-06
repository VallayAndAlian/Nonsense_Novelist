

using System.Collections.Generic;

public enum EUnitState
{
    None = 0,
    Idle,
    Attack,
    Ultra,
}

public class AIController : UnitComponent
{
    protected EUnitState mState = EUnitState.None;
    public EUnitState State => mState;

    protected AbilityBase mAutoAttackAbility = null; // 攻击技能
    protected AbilityBase mUltraAbility = null; // 特殊技能
    
    protected BattleUnit mTarget = null;
    public BattleUnit Target => mTarget;

    protected bool hasBindAnimFunc = false;

    public void EnterState(EUnitState state)
    {
        if (mState == state)
            return;

        switch (mState)
        {
            case EUnitState.Attack:
            {
                if (mAutoAttackAbility != null)
                {
                    mAutoAttackAbility.SetTarget(null);
                    mAutoAttackAbility.TryDeactivate();
                }
                
                mTarget = null;
                
                UnbindAnimFunc();
                
                break;
            }

            case EUnitState.Ultra:
            {
                if (mUltraAbility != null)
                {
                    mUltraAbility.SetTarget(null);
                    mUltraAbility.TryDeactivate();
                }
                
                mTarget = null;
                
                UnbindAnimFunc();
                
                break;
            }
        }
        
        mState = state;

        switch (mState)
        {
            case EUnitState.Idle:
            {
                if (mOwner.UnitView)
                    mOwner.UnitView.ModelLayout.Animator.Play("idle", 0, 0);

                break;
            }

            case EUnitState.Attack:
            {
                BindAnimFunc();
                
                if (mOwner.UnitView)
                    mOwner.UnitView.ModelLayout.Animator.Play("attack", 0, 0);
                
                break;
            }

            case EUnitState.Ultra:
            {
                BindAnimFunc();
                
                if (mUltraAbility != null && mOwner.UnitView != null && !mUltraAbility.AnimName.Equals("None"))
                    mOwner.UnitView.ModelLayout.Animator.Play(mUltraAbility.AnimName, 0, 0);
                
                break;
            }
        }
    }

    public override void Start()
    {
        mAutoAttackAbility = mOwner.AbilityAgent.GetAbilityByType(AbilityType.AutoAttack);
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

            case EUnitState.Ultra:
                TickUltra(deltaTime);
                break;

            default:
                break;
        }
    }

    protected void TickIdle(float deltaTime)
    {
        foreach (var abi in mOwner.AbilityAgent.Abilities)
        {
            if (!abi.IsUltra)
                continue;

            if (!abi.CanActivate())
                continue;

            mTarget = abi.PickTarget();
            if (mTarget == null)
                continue;

            abi.SetTarget(mTarget);
            if (abi.TryActivate())
            {
                mUltraAbility = abi;
                EnterState(EUnitState.Ultra);
                return;
            }
        }

        if (mAutoAttackAbility != null && mAutoAttackAbility.CanActivate())
        {
            mTarget = mAutoAttackAbility.PickTarget();

            if (mTarget != null)
            {
                mAutoAttackAbility.SetTarget(mTarget);
                if (mAutoAttackAbility.TryActivate())
                {
                    EnterState(EUnitState.Attack);
                }
            }
        }
    }

    protected void TickAttack(float deltaTime)
    {
        bool needStop = !CanKeepAttack();

        if (!needStop)
        {
            if (!IsValidTarget(mTarget))
            {
                needStop = true;
            }
            else if (mAutoAttackAbility is not { IsActivated: true })
            {
                needStop = true;
            }
        }

        if (needStop)
        {
            EnterState(EUnitState.Idle);
        }
    }
    
    protected void TickUltra(float deltaTime)
    {
        bool needStop = !CanKeepAttack();

        if (!needStop)
        {
            if (!IsValidTarget(mTarget))
            {
                needStop = true;
            }
            else if (mUltraAbility is not { IsActivated: true })
            {
                needStop = true;
            }
        }

        if (needStop)
        {
            EnterState(EUnitState.Idle);
        }
    }

    protected bool CanKeepAttack()
    {
        return true;
    }

    protected bool IsValidTarget(BattleUnit target)
    {

        if (mTarget is not { IsAlive: true })
            return false;

        return true;
    }

    protected void BindAnimFunc()
    {
        if (hasBindAnimFunc)
            return;
        
        if (mOwner.UnitView)
        {
            mOwner.UnitView.ModelLayout.AnimEvents.OnActBegin += OnActBegin;
            mOwner.UnitView.ModelLayout.AnimEvents.OnActTrigger += OnActTrigger;
            mOwner.UnitView.ModelLayout.AnimEvents.OnActEnd += OnActEnd;
        }
        
        hasBindAnimFunc = true;
    }

    protected void UnbindAnimFunc()
    {
        if (!hasBindAnimFunc)
            return;
        
        if (mOwner.UnitView)
        {
            mOwner.UnitView.ModelLayout.AnimEvents.OnActBegin -= OnActBegin;
            mOwner.UnitView.ModelLayout.AnimEvents.OnActTrigger -= OnActTrigger;
            mOwner.UnitView.ModelLayout.AnimEvents.OnActEnd -= OnActEnd;
        }

        hasBindAnimFunc = false;
    }
    
    protected void OnActBegin()
    {
        
    }
    
    protected void OnActTrigger()
    {
        switch (State)
        {
            case EUnitState.Attack:
                if (mAutoAttackAbility != null)
                {
                    mAutoAttackAbility.OnAnimTrigger();
                }
                break;

            case EUnitState.Ultra:
                if (mUltraAbility != null)
                {
                    mUltraAbility.OnAnimTrigger();
                }
                break;
        }
    }
    
    protected void OnActEnd()
    {
        EnterState(EUnitState.Idle);
    }
}
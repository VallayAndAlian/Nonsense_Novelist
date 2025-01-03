﻿
using UnityEngine;

public class AbilityTrigger : AbilityModule
{
    public enum Type
    {
        Undefined = 0,
        Direct,                                // 直接触发
    }

    public static AbilityTrigger Create(Type type)
    {
        AbilityTrigger trigger = null;

        switch (type)
        {
            case Type.Direct:
                trigger = new AMTDirect();
                break;
            
            default:
                break;
        }

        if (trigger != null)
            trigger.mType = type;
        
        return trigger;
    }
    
    
    public Type mType = Type.Undefined;
    
    protected int mMaxTriggerTimes = int.MaxValue;
    protected float mPossibility = 1.0f;
    protected float mCoolDownDuration = 0f;
    
    
    protected int mTriggerTimes = 0;
    protected float mLastTriggerTime = 0f;

    protected override int CommonArgCount => 3;

    public bool CoolDown
    {
        get
        {
            if (mTriggerTimes <= 0 || mCoolDownDuration < 0.01f)
                return true;
            else
                return /* 战斗时钟 */ - mLastTriggerTime >= mCoolDownDuration;
        }
    }
    
    public bool TriggerTimesReachLimit => mMaxTriggerTimes > 0 && mTriggerTimes >= mMaxTriggerTimes;

    protected override bool ParseParams()
    {
        mMaxTriggerTimes = Mathf.RoundToInt(GetArg(0));
        mPossibility = GetArg(1);
        mCoolDownDuration = GetArg(2);
        
        mParseIndex = 3;

        return true;
    }

    bool TryTrigger(object triggerData)
    {
        if (ShouldTrigger())
        {
            mTriggerTimes++;
            mLastTriggerTime = 0/* 战斗时钟 */;
            
            mOwner.TriggeredBy(triggerData);
            return true;
        }

        return false;
    }

    protected virtual bool ShouldTrigger()
    {
        if (!CoolDown)
            return false;

        if (TriggerTimesReachLimit)
            return false;
        
        if (mPossibility < 1 && Random.Range(0f, 1.0f) >= mPossibility)
            return false;
        
        return true;
    }
    
    public virtual void OnInit() {}
    
    public virtual void OnPreDealDamageCalc(DealDamageCalc dmgCalc) { }

    public virtual void OnPreDealDamageCalcOtherAbility(DealDamageCalc dmgCalc) { }

    public virtual void OnPreTakeDamageCalc(TakeDamageCalc dmgCalc) { }

    public virtual void OnAllyPreTakeDamageCalc(TakeDamageCalc dmgCalc) { }
    

    public virtual void OnPreDealDamage(DamageReport report) { }
    
    public virtual void OnPreDealDamageOtherAbility(DamageReport report) { }

    public virtual void OnPostDealDamage(DamageReport report) { }

    public virtual void OnPostDealDamageOtherAbility(DamageReport report) { }
    
    public virtual void OnAllyDealDamage(DamageReport report) { }

    public virtual void OnEnemyDealDamage(DamageReport report) { }
    

    public virtual void OnPreTakeDamage(DamageReport report) { }

    public virtual void OnAllyPreTakeDamage(DamageReport report) { }

    public virtual void OnPostTakeDamage(DamageReport report) { }
    
    public virtual void OnAllyTakeDamage(DamageReport report) { }

    public virtual void OnEnemyTakeDamage(DamageReport report) { }
    
    public virtual void OnPawnDeath(AbstractCharacter deceased, DamageReport report) { }
    
    public virtual void OnSelfDeath(DamageReport report) { }
}

public class AMTDirect : AbilityTrigger
{
    
}
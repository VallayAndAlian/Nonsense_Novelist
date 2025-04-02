
using UnityEngine;

public class AbilityTrigger : AbilityModule
{
    public enum Type
    {
        Undefined = 0,
        Direct = 1,                                // 直接触发
        Tick = 2,                                  // 每隔多少秒
        
        /* 伤害类 */
        AutoAttackTimes = 30,                       // 每隔几次自动攻击
        BeAutoAttackTimes = 31,                     // 每受到几次自动攻击
        TakeDamageByValue = 32,                     // 每受到几点伤害
        
        /* 死亡类 */
        AnyDeath = 100,                              // 任意单位死亡时
        SelfDeath = 101,                             // 自己死亡时
        AllyDeath = 102,                             // 友军死亡时
        EnemyDeath = 103,                            // 敌人死亡时
        ServantDeath = 104,                          // 随从死亡时
    }

    public static AbilityTrigger Create(Type type)
    {
        AbilityTrigger trigger = null;

        switch (type)
        {
            case Type.Direct:
                trigger = new AMTDirect();
                break;
            
            case Type.Tick:
                trigger = new AMTTick();
                break;
            
            case Type.AutoAttackTimes:
                trigger = new AMTAttackTimes();
                break;
            
            case Type.AnyDeath:
                trigger = new AMTAnyDeath();
                break;
            
            case Type.SelfDeath:
                trigger = new AMTSelfDeath();
                break;
            
            case Type.AllyDeath:
                trigger = new AMTAllyDeath();
                break;
            
            case Type.EnemyDeath:
                trigger = new AMTEnemyDeath();
                break;
            
            case Type.ServantDeath:
                trigger = new AMTServantDeath();
                break;
            
            default:
                break;
        }

        if (trigger != null)
            trigger.mType = type;
        
        return trigger;
    }
    
    
    public Type mType = Type.Undefined;

    public AbilityTriggerTable.Data mData = null;
    
    protected int mMaxTriggerTimes = int.MaxValue;
    protected float mPossibility = 1.0f;
    protected float mCoolDownDuration = 0f;
    
    
    protected int mTriggerTimes = 0;
    protected float mLastTriggerTime = 0f;

    public bool CoolDown
    {
        get
        {
            if (mTriggerTimes <= 0 || mCoolDownDuration < 0.01f)
                return true;
            else
                return mOwner.ElapsedSec - mLastTriggerTime >= mCoolDownDuration;
        }
    }
    
    public bool TriggerTimesReachLimit => mMaxTriggerTimes > 0 && mTriggerTimes >= mMaxTriggerTimes;

    public virtual bool Setup(AbilityTriggerTable.Data data)
    {
        mData = data;
        
        mMaxTriggerTimes = mData.mMaxTriggerTimes;
        mPossibility = mData.mPossibility;
        mCoolDownDuration = mData.mCoolDownDuration;
        
        mCustomParams = data.mCustomParams;
        
        return ParseParams();
    }

    protected bool TryTrigger(object triggerData)
    {
        if (ShouldTrigger())
        {
            mTriggerTimes++;
            mLastTriggerTime = mOwner.ElapsedSec;
            
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
    
    public virtual void OnPawnDeath(BattleUnit deceased, DamageReport report) { }
    
    public virtual void OnSelfDeath(DamageReport report) { }
}

public class AMTDirect : AbilityTrigger
{
    public override void OnInit()
    {
        TryTrigger(null);
    }
}

public class AMTTick : AbilityTrigger
{
    public override void Update(float deltaTime)
    {
        TryTrigger(null);
    }
}


public class AMTAttackTimes : AbilityTrigger
{
    protected Formula mAttackTimes = new Formula("attack_times");
    protected int mCurrentAttackTimes = 0;
    
    protected virtual void AddParams()
    {
        mParams.Add(mAttackTimes);
    }

    public override void OnPreDealDamage(DamageReport report)
    {
        if (report.mMeta.mAbility != null && report.mMeta.mAbility.Data.mType == AbilityType.AutoAttack)
        {
            ++mCurrentAttackTimes;
            if (mCurrentAttackTimes >= mAttackTimes.EvaluateInt(mOwner))
            {
                mCurrentAttackTimes = 0;
                TryTrigger(report);
            }
        }
    }
}

public class AMTAnyDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        TryTrigger(report);
    }
}

public class AMTSelfDeath : AbilityTrigger
{
    public override void OnSelfDeath(DamageReport report)
    {
        TryTrigger(report);
    }
}

public class AMTAllyDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        if (deceased.Camp == mOwner.Camp)
        {
            TryTrigger(report);
        }
    }
}

public class AMTEnemyDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        if (deceased.Camp != mOwner.Camp)
        {
            TryTrigger(report);
        }
    }
}

public class AMTServantDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        if (deceased.ServantOwner == mOwner.Unit)
        {
            TryTrigger(report);
        }
    }
}

using System.Collections.Generic;

public class BattleEffect : CoreEntity
{
    public BattleEffectTable.Data mDefine;
    
    public BattleUnit mInstigator;
    public BattleUnit mTarget;
    public AbilityBase mAbility;
    
    public EffectType mType;

    public float mTimer;
    public float mDuration;
    public float mApplyTime;
    public float mExpiredTime;

    public int mStackCount;
    public int mMaxStackCount;
    
    public bool mIgnored;
    public bool mExpired;
    
    public Status mApplyStatus;
    public List<AttributeModifier> mModifiers = new List<AttributeModifier>();

    public bool CanBePurged => mDefine.mCanBePurged;
    public bool IsRemoveOnCombatEnd => mDefine.mRemoveOnCombatEnd;

    public BattleEffect() {}
    
    public BattleEffect(BattleEffectApplier applier)
    {
        mInstigator = applier.mInstigator;
        mTarget = applier.mTarget;
        mAbility = applier.mAbility;
        
        mDefine = applier.mDefine;
        mType = applier.mDefine.mType;
        
        mDuration = applier.mDefine.mDuration;
        
        mMaxStackCount = applier.mDefine.mMaxStackCount;
        mStackCount = applier.mStackCount;

        mApplyStatus = applier.mDefine.mApplyStatus;
        
        mModifiers.Clear();
        mModifiers.AddRange(applier.mDefine.mModifiers);

        mIgnored = false;
        mExpired = false;
    }

    public void MarkInvalid()
    {
        mExpired = true;
    }
    
    public virtual void Execute() { }
    
    #region ParseCustomParams

    protected List<Formula> mParams = new List<Formula>();
    
    protected virtual void AddParams() {}
    
    public bool ParseParams()
    {
        AddParams();
        
        foreach (var param in mParams)
        {
            if (!ReadParam(param))
            {
                return false; 
            }
        }
        
        return true; 
    }
    
    protected bool ReadParam(Formula param)
    {
        if (mDefine.mCustomParams.TryGetValue(param.mKey, out var data))
        {
            param.mValues = data.mValues;
            return true;
        }
        
        return false;
    }

    #endregion
    
    
    #region DamageProcess
    
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
    
    #endregion
}



using UnityEngine;

public enum EffectType
{
    None = 0,
    Common = 1,
    Stun,
    Damage,
    
    AttackUp,
    AttackDown,
    DefUp,
    DefDown,
    SanUp,
    SanDown,
    PsyUp,
    PsyDown,
    MaxHpUp,
    MaxHpDown,
    Heal,
}
    
public enum EffectDurationRule
{
    Instant = 0,
    HasDuration,
    Script,
}
    
public enum EffectStackRule
{
    None = 0,
    Source,
    Target,
}

public enum EffectStackDurationRule
{
    None = 0,
    Refresh,
}


public class BattleEffectApplier
{
    public BattleUnit mInstigator;
    public BattleUnit mTarget;
    public AbilityBase mAbility;

    public BattleEffectTable.Data mDefine;
    
    public int mStackCount;
    public float mDuration;

    public BattleEffectApplier(int kind, AbilityBase abi)
    {
        mDefine = BattleEffectTable.Find(kind);
        if (mDefine == null)
            return;
        
        if (abi.IsValid())
        {
            mInstigator = abi.Unit;
            mAbility = abi;
        }
        else
        {
            mInstigator = null;
            mAbility = null;
        }
        
        mTarget = null;
        mStackCount = 1;
        mDuration = mDefine.mDuration;
    }
    
    public BattleEffectApplier(BattleEffectApplier other)
    {
        mDefine = other.mDefine;
        mInstigator = other.mInstigator;
        mTarget = other.mTarget;
        mAbility = other.mAbility;
        mStackCount = other.mStackCount;
    }
}

public class BattleEffectSpec
{
    public BattleUnit mInstigator;
    public BattleUnit mTarget;
    public AbilityBase mAbility;
    
    public EffectType mType;
    public EffectDurationRule mDurationRule;
    public EffectStackRule mStackRule;
    public EffectStackDurationRule mStackDurationRule;

    public bool mInputValueBool;
    public float mInputValue;
    public int mInputValueInt;
    
    public int mStackCount = 1;
    public int mMaxStackCount;
    public float mDuration;
    
    public bool mCanBePurged = true;
    public bool mMergeInputValue = false;
    public bool mIsRemoveOnCombatEnd = true;
}

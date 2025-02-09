
using System;

public class UnitInstance
{
    public int mKind;
    public CampEnum mCamp;
}

public class UnitPlacement
{
    public int mSlotIndex;
}

public class DealDamageCalc
{
    public DamageSource mDamageSource;
    public BattleUnit mInstigator;
    public BattleUnit mTarget;
    public AbilityBase mAbility;

    public bool mMagic;
    
    public DealDamageFlag mFlag;

    public float mMinAttack;
    public float mMaxAttack;
    public float mAccuracyRate;
    public float mCriticalRate;
    public float mDealDamageUp;
    public float mDealDamageDown;

    void Reset()
    {
        mDamageSource = DamageSource.None;
        mInstigator = null;
        mTarget = null;
        mAbility = null;

        mMagic = false;
        
        mFlag = 0;

        mMinAttack = 0;
        mMaxAttack = 0;

        mAccuracyRate = 0;
        mCriticalRate = 0;
        mDealDamageUp = 0;
        mDealDamageDown = 0;
    }
}

public class TakeDamageCalc
{
    public BattleUnit mInstigator;
    public AbilityBase mAbility;
    
    public bool mMagic;
    
    public DealDamageFlag mFlag;
    
    public float mDefense; // 物理防御
    public float mResistance;  // 魔法防御
    
    public float mEvasion;  // 闪避
    
    public float mTakeDamageUp;  // 受伤提升
    public float mTakeDamageDown;   // 受伤减免
    
    void Reset()
    {
        mAbility = null;
        mInstigator = null;

        mMagic = false;
        
        mFlag = 0;
        
        mDefense = 0;
        mResistance = 0;
        mEvasion = 0;
        mTakeDamageUp = 0;
        mTakeDamageDown = 0;
    }
}

public class DamageMeta
{
    public DamageSource mDamageSource;
    public BattleUnit mInstigator;
    public BattleUnit mTarget;
    public AbilityBase mAbility;
    public DealDamageFlag mFlag;

    public float mAttack;
    public float mDefense;
}

public class DamageResult
{
    public TakeDamageFlag mFlag;
    public float mDamage;
}

public class DamageReport
{
    public DamageMeta mMeta;
    public DamageResult mResult;
}

public class EmitMeta
{
    public BattleUnit mTarget;
    public AbilityBase mAbility;
    public float mDelay;
}
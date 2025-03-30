

using System.Collections.Generic;
using UnityEngine;

public class AbilityEffectApplier : AbilityModule
{
    public enum Type
    {
        None = 0,
        Test = 1,
        AttrMod = 2,
        AttrPercentMod = 3,
        Damage = 4,
        AttrDamage = 5,
    }

    public static AbilityEffectApplier Create(Type type)
    {
        AbilityEffectApplier applier = null;

        switch (type)
        {
            case Type.Test:
                applier = new AMEATest();
                break;
            
            default:
                break;
        }

        if (applier != null)
            applier.mType = type;

        return applier;
    }
    
    struct ScheduleData
    {
        public List<BattleUnit> mTargets;
        public object mTriggerData;

        public ScheduleData(List<BattleUnit> targets, object triggerData)
        {
            mTargets = targets;
            mTriggerData = triggerData;
        }
    }
    
    List<ScheduleData> mScheduleList = new List<ScheduleData>();
    const int MaxSchedulePerFrame = 64;

    public Type mType = Type.None;
    
    public AbilityEffectApplierTable.Data mData = null;
    
    protected bool mCanBePurgedOrExpelled = true;

    protected bool mHasDuration = false;

    protected float mDuration = 0;

    protected int mStackLimit = 0;
    public int StackLimit => mStackLimit;
    
    protected virtual bool DelayApply => true;

    public override void OnInit() { }
    
    public virtual bool Setup(AbilityEffectApplierTable.Data data)
    {
        mData = data;
        mCustomParams = data.mCustomParams;
        
        mCanBePurgedOrExpelled = mData.mCanBePurgedOrExpelled;
        mStackLimit = mData.mStackLimit;
        mDuration = mData.mDuration;
        mHasDuration = mDuration > 0;
        
        return ParseParams();
    }

    public void AddTask(List<BattleUnit> targets, object triggerData)
    {
        if(DelayApply)
        {
            if(mScheduleList.Count < MaxSchedulePerFrame)
            {
                mScheduleList.Add(new ScheduleData(targets, triggerData));
            }
            else
            {
                Debug.Log($"Ability kind {mOwner.Data.mKind} with applier {mType} has reached schedule limit.");
            }
        }
        else
        {
            Apply(targets, triggerData);
        }
    }

    public virtual void Apply(List<BattleUnit> targets, object triggerData) { }

    public override void Update(float deltaTime)
    {
        if(mScheduleList.Count > 0)
        {
            for(int i = 0; i < mScheduleList.Count; ++i)
            {
                Apply(mScheduleList[i].mTargets, mScheduleList[i].mTriggerData);
            }
            
            mScheduleList.Clear();
        }
        
        Tick(deltaTime);
    }
}

public class AMEATest : AbilityEffectApplier
{
    public override void Apply(List<BattleUnit> targets, object triggerData)
    {
        foreach (var tgt in targets)
        {
            Debug.Log($"Apply to target {tgt.Data.mName}");
        }
    }
}

public class AMEAAttrMod : AbilityEffectApplier
{
    protected Formula mAttrType = new Formula("attr_type");
    protected Formula mValue = new Formula("value");
    
    public override void Apply(List<BattleUnit> targets, object triggerData)
    {
        foreach (var tgt in targets)
        {
            BattleEffectSpec spec = new BattleEffectSpec();
            spec.mType = EffectType.AttrMod;
            spec.mInstigator = mOwner.Unit;
            spec.mTarget = tgt;
            spec.mInputValueInt = mAttrType.EvaluateInt(mOwner);
            spec.mInputValue = mValue.Evaluate(mOwner);
            spec.mDuration = mDuration;
            spec.mDurationRule = mHasDuration ? EffectDurationRule.HasDuration : EffectDurationRule.Script;
            spec.mStackRule = EffectStackRule.Target;
            spec.mStackDurationRule = EffectStackDurationRule.Refresh;
            spec.mAbility = mOwner;
            spec.mMaxStackCount = mStackLimit;

            EffectAgent.ApplyEffectToTarget(tgt, spec);
        }
    }
}

public class AMEAAttrPercentMod : AbilityEffectApplier
{
    protected Formula mAttrType = new Formula("attr_type");
    protected Formula mValue = new Formula("value");
    
    public override void Apply(List<BattleUnit> targets, object triggerData)
    {
        foreach (var tgt in targets)
        {
            BattleEffectSpec spec = new BattleEffectSpec();
            spec.mType = EffectType.AttrPercentMod;
            spec.mInstigator = mOwner.Unit;
            spec.mTarget = tgt;
            spec.mInputValueInt = mAttrType.EvaluateInt(mOwner);
            spec.mInputValue = mValue.Evaluate(mOwner);
            spec.mDuration = mDuration;
            spec.mDurationRule = mHasDuration ? EffectDurationRule.HasDuration : EffectDurationRule.Script;
            spec.mStackRule = EffectStackRule.Target;
            spec.mStackDurationRule = EffectStackDurationRule.Refresh;
            spec.mAbility = mOwner;
            spec.mMaxStackCount = mStackLimit;

            EffectAgent.ApplyEffectToTarget(tgt, spec);
        }
    }
}

public class AMEADamage : AbilityEffectApplier
{
    protected Formula mDamageType = new Formula("damage_type");
    protected Formula mDamageTimes = new Formula("damage_times");
    protected Formula mDamageValue = new Formula("damage_value");
    
    public override void Apply(List<BattleUnit> targets, object triggerData)
    {
        foreach (var tgt in targets)
        {
            DealDamageCalc dmg = BattleHelper.GetReusableDealDamageCalc(mOwner.Unit);
            dmg.mTarget = tgt;
            dmg.mAbility = mOwner;
            dmg.mMinAttack = mDamageValue.Evaluate(mOwner);
            dmg.mMaxAttack = dmg.mMinAttack;

            switch ((DamageType)mDamageType.EvaluateInt(mOwner))
            {
                case DamageType.Fix:
                    dmg.mFlag |= DealDamageFlag.Fixed;
                    break;
                
                case DamageType.Magic:
                    dmg.mMagic = true;
                    break;
                
                case DamageType.Psy:
                    dmg.mMagic = false;
                    break;
            }

            for (int i = 0; i < mDamageTimes.EvaluateInt(mOwner); i++)
            {
                DamageHelper.ProcessDamage(dmg);
            }
        }
    }
}

public class AMEAAttrDamage : AbilityEffectApplier
{
    protected Formula mDamageType = new Formula("damage_type");
    protected Formula mDamageTimes = new Formula("damage_times");
    protected Formula mDamageRatio = new Formula("damage_ratio");
    protected Formula mAttrType = new Formula("attr_type");
    
    public override void Apply(List<BattleUnit> targets, object triggerData)
    {
        foreach (var tgt in targets)
        {
            DealDamageCalc dmg = BattleHelper.GetReusableDealDamageCalc(mOwner.Unit);
            dmg.mDamageSource = DamageSource.Ability;
            dmg.mTarget = tgt;
            dmg.mAbility = mOwner;
            dmg.mMinAttack = mOwner.Unit.GetAttributeValue((AttributeType)mAttrType.EvaluateInt(mOwner)) * mDamageRatio.Evaluate(mOwner);
            dmg.mMaxAttack = dmg.mMinAttack;

            switch ((DamageType)mDamageType.EvaluateInt(mOwner))
            {
                case DamageType.Fix:
                    dmg.mFlag |= DealDamageFlag.Fixed;
                    break;
                
                case DamageType.Magic:
                    dmg.mMagic = true;
                    break;
                
                case DamageType.Psy:
                    dmg.mMagic = false;
                    break;
            }

            for (int i = 0; i < mDamageTimes.EvaluateInt(mOwner); i++)
            {
                DamageHelper.ProcessDamage(dmg);
            }
        }
    }
}
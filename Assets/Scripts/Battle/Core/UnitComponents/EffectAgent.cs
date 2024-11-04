

using System.Collections.Generic;

public class EffectAgent : UnitComponent
{
    protected List<BattleEffect> mEffects = new List<BattleEffect>();
    private List<BattleEffect> mRemovedEffect = new List<BattleEffect>();

    public BattleEffect ApplyEffect(BattleEffectSpec spec)
    {
        if (!CanApplyEffect(spec))
            return null;

        BattleEffect mergedBe = TryMergeEffect(spec);
        if (mergedBe != null)
            return mergedBe;

        if (spec.mDurationRule == EffectDurationRule.Instant)
        {
            
        }
        else
        {
            BattleEffect newBe = new BattleEffect(spec);

            newBe.mApplyTime = Owner.Battle.Now;
            
            if (newBe.mDurationRule == EffectDurationRule.HasDuration)
            {
                newBe.mExpiredTime = Owner.Battle.Now + spec.mDuration;
            }

            //todo: process effect apply event

            return newBe;
        }
        
        return null;
    }

    public bool CanApplyEffect(BattleEffectSpec spec)
    {
        if (spec.mCanBePurged)
        {
            if (BattleHelper.IsPositiveEffect(spec))
            {
                if (Owner.Status.InStatus(Status.BlockPositive))
                {
                    return false;
                }
            }
            else
            {
                if (Owner.Status.InStatus(Status.BlockNegative))
                {
                    return false;
                }
            }
        }
        
        //todo: consider other condition

        return true;
    }

    public BattleEffect TryMergeEffect(BattleEffectSpec spec)
    {
        if (spec.mStackRule == EffectStackRule.None)
            return null;

        BattleEffect mergedBe = null;
        foreach (var effect in mEffects)
        {
            if (effect.mExpired)
                continue;

            if (spec.mStackRule != effect.mStackRule || effect.mType != spec.mType)
                continue;

            if (spec.mStackRule == EffectStackRule.Source && spec.mInstigator != effect.mInstigator)
                continue;
            
            if (spec.mStackRule == EffectStackRule.Target && spec.mTarget != effect.mTarget)
                continue;

            mergedBe = effect;
            
            if (spec.mStackDurationRule == EffectStackDurationRule.Refresh)
            {
                mergedBe.mExpiredTime = Owner.Battle.Now + spec.mDuration;
            }

            if (spec.mMergeInputValue)
            {
                mergedBe.mInputValue = BattleHelper.MergerEffectValue(spec.mType, spec.mInputValue, effect.mInputValue);
            }
            
            break;
        }
        
        return mergedBe;
    }

    public override void LateUpdate(float deltaTime)
    {
        mRemovedEffect.Clear();
        
        foreach (var effect in mEffects)
        {
            if (effect.mExpired)
            {
                mRemovedEffect.Add(effect);
                continue;
            }

            if (effect.mDurationRule == EffectDurationRule.HasDuration && Owner.Battle.Now > effect.mExpiredTime)
            {
                mRemovedEffect.Add(effect);
                continue;
            }

            switch (effect.mType)
            {
                case EffectType.AttributeMod:
                    Owner.AddMod((AttributeType)effect.mInputValue, effect.mInputValue);
                    break;
                
                case EffectType.AttributePercentMod:
                    Owner.AddPercentMod((AttributeType)effect.mInputValue, effect.mInputValue);
                    break;
                
                default:
                    break;
            }
        }

        if (mRemovedEffect.Count > 0)
        {
            foreach (var effect in mRemovedEffect)
            {
                mEffects.Remove(effect);
            }
        
            mRemovedEffect.Clear();
        }
    }
}
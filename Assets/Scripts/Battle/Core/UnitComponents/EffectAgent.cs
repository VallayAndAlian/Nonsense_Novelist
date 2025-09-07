

using System.Collections.Generic;
using UnityEngine;

public class EffectAgent : UnitComponent
{
    protected List<BattleEffect> mEffects = new List<BattleEffect>();
    public List<BattleEffect> Effects => mEffects;
        
    private List<BattleEffect> mRemovedEffect = new List<BattleEffect>();

    public static BattleEffect ApplyEffectToTarget(BattleUnit target, BattleEffectApplier applier)
    {
        if (!target.IsValid())
            return null;
        
        applier.mTarget = target;
        return target.EffectAgent?.ApplyEffect(applier);
    }

    protected BattleEffect ApplyEffect(BattleEffectApplier applier)
    {
        if (!CanApplyEffect(applier))
            return null;

        BattleEffect mergedBe = TryMergeEffect(applier);
        if (mergedBe != null)
            return mergedBe;
        
        BattleEffect newBe = EffectFactory.CreateEffect(applier);
        if (applier.mDefine.mDurationRule == EffectDurationRule.Instant)
        {
            newBe.Execute();
            newBe.Dispose();
            return null;
        }
        else
        {
            newBe.mApplyTime = Owner.Battle.Now;
            
            if (newBe.mDefine.mDurationRule == EffectDurationRule.HasDuration)
            {
                newBe.mExpiredTime = Owner.Battle.Now + applier.mDuration;
                
                if (applier.mInstigator.IsValid() && BattleHelper.IsNegativeEffect(applier))
                {
                    newBe.mExpiredTime = Owner.Battle.Now + applier.mDuration * (1 + applier.mInstigator.GetAttributeValue(AttributeType.DebuffUp));
                }
            }
            
            mEffects.Add(newBe);
            mOwner.OnSelfApplyEffect(newBe);
            
            mOwner.UnitView.OnApplyEffect(newBe);
            
            EventManager.Invoke(EventEnum.ApplyEffect, newBe);

            return newBe;
        }
        
        //return null;
    }

    public bool CanApplyEffect(BattleEffectApplier applier)
    {
        if (applier.mDefine == null)
            return false;
        
        if (applier.mDefine.mCanBePurged)
        {
            if (BattleHelper.IsPositiveEffect(applier))
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

    public BattleEffect TryMergeEffect(BattleEffectApplier applier)
    {
        if (applier.mDefine.mStackRule == EffectStackRule.None)
            return null;

        BattleEffect mergedBe = null;
        foreach (var effect in mEffects)
        {
            if (effect.mExpired)
                continue;

            if (effect.mType != applier.mDefine.mType || effect.mAbility != applier.mAbility)
                continue;

            if (applier.mDefine.mStackRule != effect.mDefine.mStackRule || effect.mDefine.mStackDurationRule != applier.mDefine.mStackDurationRule)
                continue;

            if (applier.mDefine.mStackRule == EffectStackRule.Source && applier.mInstigator != effect.mInstigator)
                continue;
            
            if (applier.mDefine.mStackRule == EffectStackRule.Target && applier.mTarget != effect.mTarget)
                continue;

            mergedBe = effect;
            
            if (applier.mDefine.mStackDurationRule == EffectStackDurationRule.Refresh)
            {
                mergedBe.mExpiredTime = Owner.Battle.Now + applier.mDefine.mDuration;
            }
            
            if (mergedBe.mMaxStackCount == 0 || mergedBe.mStackCount < mergedBe.mMaxStackCount)
            {
                if (mergedBe.mMaxStackCount > 0)
                {
                    mergedBe.mStackCount = Mathf.Clamp(mergedBe.mStackCount + applier.mStackCount, 0, mergedBe.mMaxStackCount);
                }
                else
                {
                    ++mergedBe.mStackCount;
                }
            }
            
            break;
        }
        
        return mergedBe;
    }

    public void ClearEffects()
    {
        foreach (var effect in mEffects)
        {
            mOwner.UnitView.OnRemoveEffect(effect);

            EventManager.Invoke(EventEnum.RemoveEffect, effect);

            effect.Dispose();
        }

        mEffects.Clear();
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

            if (effect.mDefine.mDurationRule == EffectDurationRule.HasDuration &&
                Owner.Battle.Now > effect.mExpiredTime)
            {
                mRemovedEffect.Add(effect);
                continue;
            }

            if (effect.mDefine.mDurationRule == EffectDurationRule.Script && !effect.mAbility.IsValid())
            {
                mRemovedEffect.Add(effect);
                continue;
            }

            if (effect.IsRemoveOnCombatEnd && Owner.Battle.BattlePhase.IsCombat == false)
            {
                mRemovedEffect.Add(effect);
                continue;
            }

            foreach (var modifier in effect.mModifiers)
            {
                if (modifier.mPercent)
                {
                    Owner.AddPercentMod(modifier.mType, modifier.mValue);
                }
                else
                {
                    Owner.AddMod(modifier.mType, modifier.mValue);
                }
            }
            
            Owner.Status.AddStatus(effect.mApplyStatus);

            if (effect.mDefine.mTickInterval >= 0)
            {
                effect.mTimer -= deltaTime;
                if (effect.mTimer <= effect.mDefine.mTickInterval)
                {
                    effect.mTimer = effect.mDefine.mTickInterval;
                    effect.Execute();
                }
            }
        }

        if (mRemovedEffect.Count > 0)
        {
            foreach (var effect in mRemovedEffect)
            {
                mOwner.UnitView.OnRemoveEffect(effect);

                EventManager.Invoke(EventEnum.RemoveEffect, effect);

                mEffects.Remove(effect);

                effect.Dispose();
            }

            mRemovedEffect.Clear();
        }
    }

    public override void OnSelfDeath(DamageReport report)
    {
        foreach (var abi in Effects)
        {
            abi.OnSelfDeath(report);
        }
        
        ClearEffects();
    }
    
    #region DamageProcess

    /// <summary>
    /// 造成伤害计算预处理
    /// </summary>
    public void PreDealDamageCalc(DealDamageCalc damageCalc)
    {
        if ((damageCalc.mFlag & DealDamageFlag.Fixed) == 0)
        {
            foreach (var effect in Effects)
            {
                if (effect.mAbility == damageCalc.mAbility)
                {
                    effect.OnPreDealDamageCalc(damageCalc);
                }
                else
                {
                    effect.OnPreDealDamageCalcOtherAbility(damageCalc);
                }
            }
        }
    }

    public void OnPreTakeDamageCalc(TakeDamageCalc dmgCalc)
    {
        foreach (var effect in Effects)
        {
            effect.OnPreTakeDamageCalc(dmgCalc);
        }
    }

    public void OnAllyPreTakeDamageCalc(TakeDamageCalc dmgCalc)
    {
        foreach (var effect in Effects)
        {
            effect.OnAllyPreTakeDamageCalc(dmgCalc);
        }
    }

    public void PreDealDamage(DamageReport report)
    {
        foreach (var effect in Effects)
        {
            if (report.mMeta.mAbility == effect.mAbility)
            {
                effect.OnPreDealDamage(report);
            }
            else
            {
                effect.OnPreDealDamageOtherAbility(report);
            }
        }
    }

    public void PreTakeDamage(DamageReport report)
    {
        foreach (var effect in Effects)
        {
            effect.OnPreTakeDamage(report);
        }
    }

    public void PostDealDamage(DamageReport report)
    {
        foreach (var effect in Effects)
        {
            if (effect.mAbility == report.mMeta.mAbility)
            {
                effect.OnPostDealDamage(report);
            }
            else
            {
                effect.OnPostDealDamageOtherAbility(report);
            }
        }
    }

    public void PostTakeDamage(DamageReport report)
    {
        foreach (var effect in Effects)
        {
            effect.OnPostTakeDamage(report);
        }
    }

    public void AllyDealDamage(DamageReport report)
    {
        foreach (var effect in Effects)
        {
            effect.OnAllyDealDamage(report);
        }
    }

    public void AllyTakeDamage(DamageReport report)
    {
        foreach (var effect in Effects)
        {
            effect.OnAllyTakeDamage(report);
        }
    }

    public void EnemyDealDamage(DamageReport report)
    {
        foreach (var effect in Effects)
        {
            effect.OnEnemyDealDamage(report);
        }
    }

    public void EnemyTakeDamage(DamageReport report)
    {
        foreach (var effect in Effects)
        {
            effect.OnEnemyTakeDamage(report);
        }
    }

    #endregion

    public override void OnExitCombatPhase()
    {
        // 清除buff
        ClearEffects();
    }
}
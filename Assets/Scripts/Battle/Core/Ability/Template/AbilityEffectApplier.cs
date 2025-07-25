﻿

using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Spine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.CanvasScaler;

public class AbilityEffectApplier : AbilityModule
{
    public enum Type
    {
        None = 0,
        Test = 1,
        AttrMod = 2,
        Damage = 3,
        AttrDamage = 4,
        Reincarnation=5,
        AttrHeal=6,
        KillServant=7,
        SeizeServant=8,
        DrawCertainEffect=9,
        TestSeizeServant=10,
    }

    public static AbilityEffectApplier Create(Type type)
    {
        AbilityEffectApplier applier = null;

        switch (type)
        {
            case Type.Test:
                applier = new AMEATest();
                break;
            
            case Type.AttrMod:
                applier = new AMEAAttrMod();
                break;
            
            case Type.Damage:
                applier = new AMEADamage();
                break;
            
            case Type.AttrDamage:
                applier = new AMEAAttrDamage();
                break;

            case Type.Reincarnation:
                applier = new AMEAReincarnation();
                break;

            case Type.AttrHeal:
                applier = new AMEAAttrHeal();
                break;

            case Type.KillServant:
                applier = new AMEAKillServant();
                break;

            case Type.SeizeServant:
                applier = new AMEASeizeServant();
                break;

            case Type.DrawCertainEffect:
                applier = new AMEADrawCertainEffect();
                break;

            case Type.TestSeizeServant:
                applier= new AMEATestSeizeServant();
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

    protected bool mIsRemoveOnCombatEnd=true;

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
        mIsRemoveOnCombatEnd = mData.mIsRemoveOnCombatEnd;
        
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

    public virtual void Apply(List<BattleUnit> targets, object triggerData)
    {
        if (mOwner.Data.mProjKind > 0)
        {
            foreach (var tgt in targets)
            {
                EmitMeta meta = new EmitMeta();
                meta.mProjKind = mOwner.Data.mProjKind;
                meta.mInstigator = mOwner.Unit;
                meta.mTarget = tgt;
                meta.mAbility = mOwner;
                meta.mHitCallBack = (tgt1) => Apply(tgt1, triggerData);

                BattleObjectFactory.StartEmit(meta);
            }
        }
        else
        {
            foreach (var tgt in targets)
            {
                Apply(tgt, triggerData);
            }
        }
    }

    public List<BattleUnit> DrawRandomItems(List<BattleUnit> list, int count)
    {
        if (count <= 0 || list.Count == 0)
            return new List<BattleUnit>();

        // 限制最大抽取数量
        int drawCount = Mathf.Min(count, list.Count);
        List<BattleUnit> drawnItems = new List<BattleUnit>(drawCount);
        for (int i = 0; i < drawCount; i++)
        {
            // 随机选择一个索引
            int randomIndex = UnityEngine.Random.Range(0, list.Count);

            // 将随机元素添加到结果列表
            drawnItems.Add(list[randomIndex]);

            // 将随机元素与最后一个元素交换
            BattleUnit temp = list[randomIndex];
            list[randomIndex] = list[list.Count - 1];
            list[list.Count - 1] = temp;

            // 移除最后一个元素（即被选中的元素）
            list.RemoveAt(list.Count - 1);
        }

        return drawnItems;
    }

    public virtual void Apply(BattleUnit target, object triggerData) { }

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
    public override void Apply(BattleUnit target, object triggerData)
    {
        Debug.Log($"Apply to target {target.Data.mName}");
    }
}

public class AMEAAttrMod : AbilityEffectApplier
{
    protected Formula mAttrType = new Formula("attr_type");
    protected Formula mValue = new Formula("value");
    protected Formula mModifyPercent = new Formula("is_percent");

    public override void AddParams()
    {
        mParams.Add(mAttrType);
        mParams.Add(mValue);
        mParams.Add(mModifyPercent);
    }

    public override void Apply(BattleUnit target, object triggerData)
    {
        BattleEffectSpec spec = new BattleEffectSpec();
        spec.mType = BattleHelper.ToEffectType((AttributeType)mAttrType.EvaluateInt(mOwner), mValue.Evaluate(mOwner) > 0);
        spec.mInstigator = mOwner.Unit;
        spec.mTarget = target;
        spec.mInputValueBool = mModifyPercent.EvaluateBool(mOwner);
        spec.mInputValue = mValue.Evaluate(mOwner);
        spec.mDuration = mDuration;
        spec.mDurationRule = mHasDuration ? EffectDurationRule.HasDuration : EffectDurationRule.Script;
        spec.mStackRule = EffectStackRule.Target;
        spec.mStackDurationRule = EffectStackDurationRule.Refresh;
        spec.mAbility = mOwner;
        spec.mMaxStackCount = mStackLimit;
        spec.mIsRemoveOnCombatEnd = mIsRemoveOnCombatEnd;
        EffectAgent.ApplyEffectToTarget(target, spec);
    }
}

public class AMEADamage : AbilityEffectApplier
{
    protected Formula mDamageType = new Formula("damage_type");
    protected Formula mDamageTimes = new Formula("damage_times");
    protected Formula mDamageValue = new Formula("damage_value");
    
    public override void AddParams()
    {
        mParams.Add(mDamageType);
        mParams.Add(mDamageTimes);
        mParams.Add(mDamageValue);
    }

    public override void Apply(BattleUnit target, object triggerData)
    {
        DealDamageCalc dmg = BattleHelper.GetReusableDealDamageCalc(mOwner.Unit);
        dmg.mTarget = target;
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

public class AMEAAttrDamage : AbilityEffectApplier
{
    protected Formula mDamageType = new Formula("damage_type");
    protected Formula mDamageTimes = new Formula("damage_times");
    protected Formula mDamageRatio = new Formula("damage_ratio");
    protected Formula mAttrType = new Formula("attr_type");
    
    public override void AddParams()
    {
        mParams.Add(mDamageType);
        mParams.Add(mDamageTimes);
        mParams.Add(mDamageRatio);
        mParams.Add(mAttrType);
    }

    public override void Apply(BattleUnit target, object triggerData)
    {
        DealDamageCalc dmg = BattleHelper.GetReusableDealDamageCalc(mOwner.Unit);
        dmg.mDamageSource = DamageSource.Ability;
        dmg.mTarget = target;
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
public class AMEAReincarnation : AbilityEffectApplier
{
    protected override bool DelayApply => false;

    public override void Apply(BattleUnit target, object triggerData)
    {
        target.ServantOwner.ServantsAgent.RegisterServants(target.Data.mKind);
        target.ModifyBase(AttributeType.MaxHp, -0.7f, true);
    }
}
public class AMEAAttrHeal : AbilityEffectApplier
{
    protected Formula mAttrType = new Formula("attr_type");
    protected Formula mValue = new Formula("value");
    protected Formula mModifyPercent = new Formula("is_percent");
    protected float mCurrentHeal=0;

    public override void AddParams()
    {
        mParams.Add(mAttrType);
        mParams.Add(mValue);
        mParams.Add(mModifyPercent);
    }

    public override void Apply(BattleUnit target, object triggerData)
    {
        if(mModifyPercent.EvaluateBool(mOwner))
        {
            mCurrentHeal=mOwner.Unit.GetAttributeValue((AttributeType)mAttrType.EvaluateInt(mOwner))*mValue.Evaluate(mOwner);
        }
        else
        {
            mCurrentHeal=mValue.Evaluate(mOwner);
        }
        target.ApplyHeal(mCurrentHeal);
    }
}
public class AMEAKillServant : AbilityEffectApplier
{
    protected Formula mSelectMethod = new Formula("select_method");
    protected Formula mKillNum = new Formula("kill_num");
    protected List<BattleUnit> mServants = null;
    protected BattleUnit mMaxServantunit = null;
    public override void AddParams()
    {
        mParams.Add(mSelectMethod);
        mParams.Add(mKillNum);
    }
    public override void Apply(BattleUnit target, object triggerData)
    {
        if(mKillNum.EvaluateInt(mOwner)!=0)
        {
            if (mSelectMethod.EvaluateInt(mOwner) == 1)
            {
                mServants = mOwner.Unit.ServantsAgent.GetEnemiesServantList(mOwner.Unit);
                var s = DrawRandomItems(mServants, mKillNum.EvaluateInt(mOwner));
                foreach (var unit in s)
                {
                    unit.Die(new DamageReport());
                }
            }else if (mSelectMethod.EvaluateInt(mOwner) == 2)
            {
                mMaxServantunit=mOwner.Unit.ServantsAgent.GetEnemyMaxServantUnit(mOwner.Unit);
                List<UnitSlot> slots = mMaxServantunit.UnitView.Root.Find("Servants").GetComponentsInChildren<UnitSlot>().ToList();
                if (mMaxServantunit.ServantsAgent.Servants.Count> mKillNum.EvaluateInt(mOwner))
                {
                    for (int i = slots.Count - 1; i >= 0 && mKillNum.EvaluateInt(mOwner) > 0; i--)
                    {
                        var denum = mKillNum.EvaluateInt(mOwner);
                        if (slots[i].IsOccupied==true) continue;
                        slots[i].Unit.Die(new DamageReport());
                        denum--;
                        if (denum <= 0) break;
                    }
                }
                else
                {
                    foreach (UnitSlot slot in slots)
                    {
                        if (slot.IsOccupied)
                        {
                            slot.Unit.Die(new DamageReport());
                        }
                    }
                }
            }
          
        }
    }
}

public class AMEASeizeServant : AbilityEffectApplier
{
    protected Formula mSelectMethod = new Formula("select_method");
    protected Formula mSeizeNum = new Formula("seize_num");
    protected Formula mIsReset = new Formula("is_reset");
    protected Formula mAttrAdjValue = new Formula("attrAdj_value");
    protected List<BattleUnit> mServants=null;
    protected BattleUnit mMaxServantunit= null;
    public override void AddParams()
    {
        mParams.Add(mSelectMethod);
        mParams.Add(mSeizeNum);
        mParams.Add(mIsReset);
        mParams.Add(mAttrAdjValue);
    }
    public override void Apply(BattleUnit target, object triggerData)
    {
        if (mOwner.Unit.ServantsAgent.Servants.Count >= 3|| mOwner.Unit.ServantsAgent.Servants.Count+mSeizeNum.EvaluateInt(mOwner)>3)
        {
            Debug.LogError("随从数量已达到最大值，无法夺取随从！");
            return;
        }
        else 
        {
            if (mSelectMethod.EvaluateInt(mOwner) == 1)
            {
                mServants = mOwner.Unit.ServantsAgent.GetEnemiesServantList(mOwner.Unit); ;
                var s = DrawRandomItems(mServants, mSeizeNum.EvaluateInt(mOwner));
                foreach (var servant in s)
                {
                    ResetServant(servant, target);
                }
            }
            else if (mSelectMethod.EvaluateInt(mOwner) == 2)
            {
                mMaxServantunit = mOwner.Unit.ServantsAgent.GetEnemyMaxServantUnit(mOwner.Unit);
                List<UnitSlot> slots = mMaxServantunit.UnitView.Root.Find("Servants").GetComponentsInChildren<UnitSlot>().ToList();
                if (mMaxServantunit.ServantsAgent.Servants.Count > mSeizeNum.EvaluateInt(mOwner))
                {
                    for (int i = slots.Count - 1; i >= 0 && mSeizeNum.EvaluateInt(mOwner) > 0; i--)
                    {
                        var denum = mSeizeNum.EvaluateInt(mOwner);
                        if (slots[i].IsOccupied == true) continue;
                        ResetServant(slots[i].Unit, target);
                        denum--;
                        if (denum <= 0) break;
                    }
                }
                else
                {
                    foreach (UnitSlot slot in slots)
                    {
                        if (slot.IsOccupied)
                        {
                            ResetServant(slot.Unit,target );
                        }
                    }
                }
            }

        }
    }
    public void ResetServant(BattleUnit servant,BattleUnit target)
    {
        if (mIsReset.EvaluateBool(mOwner))
        {
            servant.ServantOwner.ServantsAgent.RemoveServants(servant);
            var newservant = target.ServantsAgent.RegisterServants(servant.ID);
            newservant.ModifyBase(AttributeType.MaxHp,mAttrAdjValue.Evaluate(mOwner),true);
        }
        else
        {
            servant.ModifyBase(AttributeType.MaxHp, mAttrAdjValue.Evaluate(mOwner), true);
            servant.ServantOwner.ServantsAgent.RemoveServants(servant);
            target.ServantsAgent.Servants.Add(servant);
            SwapSlot(target);
        }
    }
    public void SwapSlot(BattleUnit target)
    {
        var orislot = mOwner.Unit.Slot;
        var targetslot = target.Slot;
        var s = target.UnitView.Root.Find("Servants");
        List<UnitSlot> d = s.GetComponentsInChildren<UnitSlot>().ToList();
        foreach (UnitSlot slot in d)
        {
            if (!slot.IsOccupied)
            {
                orislot.Remove();
                slot.OccupiedBy(target);

                Debug.Log(slot.name);
                break;
            }
        }
        orislot.Remove();
        targetslot.Remove();
        orislot.OccupiedBy(target);
        targetslot.OccupiedBy(mOwner.Unit);
    }
}
public class AMEADrawCertainEffect : AbilityEffectApplier
{
    public enum DrawType
    {
        None = 0,
        Word = 1,
        Servant = 2,
        Buff = 3,
    }
    protected Formula mDrawType = new Formula("draw_type");
    protected Formula mDrawNum = new Formula("draw_num");
    protected Formula mGroupNum = new Formula("group_num");
    protected WeightedLottery<float> mLottery = new WeightedLottery<float>();
    List<float> mDrawObjects = new List<float>();
    public override void AddParams()
    {
        mParams.Add(mDrawType);
        mParams.Add(mDrawNum);
        mParams.Add(mGroupNum);
        for (int i = 0; i < mParams.Count; i++)
        {
            Formula param = mParams[i];
            if (!ReadParam(param))
            {
                return;
            }
            if (param.mKey == "group_num"&&param.EvaluateInt()>0)
            {
                for (int j = 1; j <=param.EvaluateInt(); j++)
                {
                    string s = string.Format("item_{0}_weight", j);
                    string t = string.Format("item_{0}", j);
                    mParams.Add(new Formula(s)); 
                    mParams.Add(new Formula(t)); 
                }
            }
        }
    }
    public override void OnInit()
    {
        for (int i = 3; i < mParams.Count; i += 2)
        {
            Formula param = mParams[i];
            mLottery.AddPool(param.mValues[0], mParams[i + 1].mValues);
        }
        GetObjectList();
    }
    public override void Apply(BattleUnit target, object triggerData)
    {
        mOwner.Battle.Clock.TimerManager.AddTimer<BattleUnit>(OnTrigger, target, 1, 0, 1);
    }
    public void ApplyWord(BattleUnit unit)
    {
        if (mDrawObjects != null)
        {
            foreach (var item in mDrawObjects)
            {
                unit.WordComponent.AddWord((int)item);
            }
        }
    }
    public void ApplyServant(BattleUnit unit)
    {
        if (mDrawObjects != null)
        {
            foreach (var item in mDrawObjects)
            {
                unit.ServantsAgent.RegisterServants((int)item);
            }
        }
    }
    public void ApplyBuff(BattleUnit unit)
    {
        if (mDrawObjects != null)
        {
            foreach (var item in mDrawObjects)
            {
                unit.AbilityAgent.RegisterAbility((int)item);
            }
        }
    }
    public List<float> GetObjectList()
    {
        for (int i = 1; i <= mDrawNum.EvaluateInt(mOwner); i++)
        {
            float item = mLottery.Draw();
            mDrawObjects.Add(item);
        }
        if (mDrawObjects.Count > 0)
        {
            return mDrawObjects;
        }
        return null;
    }
    public void OnTrigger(BattleUnit unit)
    {
        var type = (DrawType)mDrawType.EvaluateInt(mOwner);
        switch (type)
        {
            case DrawType.Word:
                ApplyWord(unit);
                break;

            case DrawType.Servant:
                ApplyServant(unit);
                break;

            case DrawType.Buff:
                ApplyBuff(unit);
                break;

            default:
                break;
        }
    }
}
public class AMEATestSeizeServant : AbilityEffectApplier { }
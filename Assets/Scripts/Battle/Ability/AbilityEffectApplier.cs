﻿

using System.Collections.Generic;
using UnityEngine;

public class AbilityEffectApplier : AbilityModule
{
    public enum Type
    {
        None = 0,
        Test
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
        public List<AbstractCharacter> mTargets;
        public object mTriggerData;

        public ScheduleData(List<AbstractCharacter> targets, object triggerData)
        {
            mTargets = targets;
            mTriggerData = triggerData;
        }
    }
    
    List<ScheduleData> mScheduleList = new List<ScheduleData>();
    const int MaxSchedulePerFrame = 64;

    public Type mType = Type.None;
    
    protected virtual bool DelayApply => true;

    public void AddTask(List<AbstractCharacter> targets, object triggerData)
    {
        if(DelayApply)
        {
            if(mScheduleList.Count < MaxSchedulePerFrame)
            {
                mScheduleList.Add(new ScheduleData(targets, triggerData));
            }
            else
            {
                // Debug.Log("Ability kind {0} with applier {1} has reached schedule limit.", mOwner.data.mKind, mType);
            }
        }
        else
        {
            Apply(targets, triggerData);
        }
    }

    public virtual void Apply(List<AbstractCharacter> targets, object triggerData) { }

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
    
}


using System.Collections.Generic;
using UnityEngine;

public class AbilityAutoAttack : AbilityBase
{
    public class DamageSchedule
    {
        public BattleUnit mTarget = null;
        public float mApplySec = 0;
    }

    protected List<DamageSchedule> mSchedules = new List<DamageSchedule>();
    protected List<DamageSchedule> mRemovedSchedules = new List<DamageSchedule>();

    protected override void OnActivate()
    {
        UnitView.ModelLayout.AnimEvents.OnActBegin += OnActBegin;
        UnitView.ModelLayout.AnimEvents.OnActTrigger += OnActTrigger;
        UnitView.ModelLayout.AnimEvents.OnActEnd += OnActEnd;
    }

    protected override void OnDeactivate()
    {
        UnitView.ModelLayout.AnimEvents.OnActBegin -= OnActBegin;
        UnitView.ModelLayout.AnimEvents.OnActTrigger -= OnActTrigger;
        UnitView.ModelLayout.AnimEvents.OnActEnd -= OnActEnd;

        OnActEnd();
    }

    protected override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        
        foreach (var schedule in mSchedules)
        {
            if (schedule.mApplySec > ElapsedSec)
                continue;

            if (schedule.mTarget != null)
            {
                // apply damage
                DealDamageCalc dmg = BattleHelper.GetReusableDealDamageCalc(Unit);
                dmg.mTarget = schedule.mTarget;
                dmg.mAbility = this;
                dmg.mMinAttack = Unit.GetAttributeValue(AttributeType.Attack);
                dmg.mMaxAttack = dmg.mMinAttack;

                DamageHelper.ProcessDamage(dmg);
            }

            mRemovedSchedules.Add(schedule);
        }

        foreach (var schedule in mRemovedSchedules)
        {
            mSchedules.Remove(schedule);
        }
        
        mRemovedSchedules.Clear();
    }

    protected void OnActBegin()
    {
        
    }
    
    protected void OnActTrigger()
    {
        if (mTarget == null)
            return;

        // emit projectile
        EmitMeta meta = new EmitMeta();
        meta.mTarget = mTarget;
        meta.mAbility = this;
        meta.mDelay = 1.0f;
        
        UnitView.OnStartEmit(meta);

        DamageSchedule schedule = new DamageSchedule();
        schedule.mTarget = mTarget;
        schedule.mApplySec = ElapsedSec + 1.0f;
        
        mSchedules.Add(schedule);
    }
    
    protected void OnActEnd()
    {
        
    }

    public override BattleUnit PickTarget()
    {
        var enemies = Unit.Enemies;
        if (enemies.Count > 0)
        {
            return enemies[Random.Range(0, enemies.Count)];
        }
        
        return null;
    }
}
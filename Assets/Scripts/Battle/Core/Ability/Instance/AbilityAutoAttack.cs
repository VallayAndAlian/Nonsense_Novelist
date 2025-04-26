

using System.Collections.Generic;
using UnityEngine;

public class AbilityAutoAttack : AbilityActive
{
    public override float CD => mData.mCoolDown * Unit.GetAttributeValue(AttributeType.Attack);

    protected override void OnInit()
    {
        base.OnInit();
        Unit.AIAgent.RegisterAttackAbility(this);
    }

    protected override void OnActivate()
    {
        
    }

    protected override void OnDeactivate()
    {
        
    }
    
    public override void OnAnimTrigger()
    {
        if (mTarget == null)
            return;

        // emit projectile
        EmitMeta meta = new EmitMeta();
        meta.mProjKind = mData.mProjKind;
        meta.mInstigator = mUnit;
        meta.mTarget = mTarget;
        meta.mAbility = this;
        meta.mHitCallBack = OnHitTarget;
        
        BattleObjectFactory.StartEmit(meta);
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

    public void OnHitTarget(BattleUnit hitTarget)
    {
        if (hitTarget is not { IsAlive: true })
            return;
        
        // apply damage
        DealDamageCalc dmg = BattleHelper.GetReusableDealDamageCalc(Unit);
        dmg.mDamageSource = DamageSource.Ability;
        dmg.mTarget = hitTarget;
        dmg.mAbility = this;
        dmg.mMinAttack = Unit.GetAttributeValue(AttributeType.Attack);
        dmg.mMaxAttack = dmg.mMinAttack;

        DamageHelper.ProcessDamage(dmg);
    }
}

using System.Collections.Generic;



public class AbilityTemplate : AbilityBase
{
    protected AbilityTrigger mTrigger = null;
    protected AbilityTargetSelector mSelector = null;
    protected List<AbilityEffectApplier> mAppliers = new List<AbilityEffectApplier>();
    
    public AbilityTemplate(AbilityTrigger trigger, AbilityTargetSelector selector, List<AbilityEffectApplier> appliers)
    {
        mTrigger = trigger;
        mSelector = selector;
        mAppliers.AddRange(appliers);

        mTrigger.Bind(this);
        mSelector.Bind(this);

        foreach (var applier in mAppliers)
        {
            applier.Bind(this);
        }

        // mStackLimit = InitStackLimit(appliers);
    }

    public void TriggeredBy(object triggerData)
    {
        List<AbstractCharacter> targets = null;

        if (mSelector != null)
            targets = mSelector.Pick(triggerData);

        foreach (var applier in mAppliers)
        {
            applier.AddTask(targets, triggerData);
        }
    }

    protected override void Tick(float deltaTime)
    {
        mTrigger?.Update(deltaTime);
    }

    protected override void TickEvenWhenDead(float deltaTime)
    {
        for (int i = 0; i < mAppliers.Count; ++i)
        {
            mAppliers[i].Update(deltaTime);
        }
    }
    
    #region TriggerEvents

    protected override void OnInit()
    {
        mTrigger.OnInit();

        foreach (var applier in mAppliers)
        {
            applier.OnInit();
        }
    }

    public override void OnPreDealDamageCalc(DealDamageCalc dmgCalc)
    {
        mTrigger.OnPreDealDamageCalc(dmgCalc);
    }

    public override void OnPreDealDamageCalcOtherAbility(DealDamageCalc dmgCalc)
    {
        mTrigger.OnPreDealDamageCalcOtherAbility(dmgCalc);
    }

    public override void OnPreDealDamage(DamageReport report)
    {
        mTrigger.OnPreDealDamage(report);
    }

    public override void OnPreDealDamageOtherAbility(DamageReport report)
    {
        mTrigger.OnPreDealDamageOtherAbility(report);
    }

    public override void OnPreTakeDamageCalc(TakeDamageCalc dmgCalc)
    {
        mTrigger.OnPreTakeDamageCalc(dmgCalc);
    }

    public override void OnAllyPreTakeDamageCalc(TakeDamageCalc dmgCalc)
    {
        mTrigger.OnAllyPreTakeDamageCalc(dmgCalc);
    }

    public override void OnPreTakeDamage(DamageReport report)
    {
        mTrigger.OnPreTakeDamage(report);
    }

    public override void OnPostDealDamage(DamageReport dmg)
    {
        mTrigger.OnPostDealDamage(dmg);
    }

    public override void OnPostDealDamageOtherAbility(DamageReport dmg)
    {
        mTrigger.OnPostDealDamageOtherAbility(dmg);
    }

    public override void OnPostTakeDamage(DamageReport dmg)
    {
        mTrigger.OnPostTakeDamage(dmg);
    }

    public override void OnAllyDealDamage(DamageReport dmg)
    {
        mTrigger.OnAllyDealDamage(dmg);
    }

    public override void OnPawnDeath(AbstractCharacter deceased, DamageReport dmg)
    {
        mTrigger.OnPawnDeath(deceased, dmg);
    }

    public override void OnSelfDeath(DamageReport dmg)
    {
        mTrigger.OnSelfDeath(dmg);
    }

    #endregion
}

public class AbilityModule
{
    protected AbilityTemplate mOwner = null;

    protected List<float> mArgs = new List<float>();
    protected List<float> mInitArgs = new List<float>();

    protected int mParseIndex = 0;

    protected virtual int CommonArgCount => 0;

    public void Bind(AbilityTemplate owner)
    {
        mOwner = owner;
    }

    public bool Setup(List<float> args)
    {
        if (args == null || args.Count < CommonArgCount)
            return false;
        
        mArgs.Clear();
        mArgs.AddRange(args);
        
        mInitArgs.Clear();
        mInitArgs.AddRange(args);
        
        return ParseParams();
    }
    
    public float GetArg(int index)
    {
        if (index >= 0 && index < mArgs.Count)
            return mArgs[index];
        else
            return 0;
    }

    protected virtual bool ParseParams() { return true; }

    public virtual void Update(float deltaTime)
    {
        Tick(deltaTime);
    }

    protected virtual void Tick(float deltaTime) {}
}
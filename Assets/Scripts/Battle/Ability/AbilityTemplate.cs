
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
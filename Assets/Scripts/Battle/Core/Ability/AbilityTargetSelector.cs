
using UnityEngine;
using System.Collections.Generic;

public class AbilityTargetSelector : AbilityModule
{
    public enum Type
    {
        None = 0,
        Instigator,
        Target,
        Enemy,
        Alley,
    }

    public static AbilityTargetSelector Create(Type type)
    {
        AbilityTargetSelector selector = null;
        
        switch (type)
        {
            case Type.Instigator:
                selector = new AMTSInstigator();
                break;
            
            case Type.Target:
                selector = new AMTSTarget();
                break;
        }

        if (selector != null)
            selector.mType = type;

        return selector;
    }
    protected int mTargetCount = 1;
    public Type mType = Type.None;

    protected override bool ParseParams()
    {
        mTargetCount = Mathf.RoundToInt(GetArg(0));


        mParseIndex = 1;
        return true;
    }

    public virtual List<AbstractCharacter> Pick(object triggerData)
    {
        return null;
    }

}

public class AMTSInstigator : AbilityTargetSelector
{
    
}

public class AMTSTarget : AbilityTargetSelector
{
    
}


using System.Collections.Generic;

public class AbilityTargetSelector : AbilityModule
{
    public enum Type
    {
        None = 0,
        Instigator,
        Target,
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

    public Type mType = Type.None;
    
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
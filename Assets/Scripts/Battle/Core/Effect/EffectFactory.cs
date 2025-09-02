

using System;
using System.Collections.Generic;
using System.Linq;

public class EffectFactory
{
    private static Dictionary<EffectType, System.Type> mEffectClassMap = new Dictionary<EffectType, System.Type>()
    {
        { EffectType.Common, typeof(BattleEffect) },
    };

    public static BattleEffect CreateEffect(BattleEffectApplier applier)
    {
        var data = applier.mDefine;
        if (data == null)
            return null;
        
        if (!mEffectClassMap.TryGetValue(data.mType, out var type))
            return null;

        BattleEffect effect = (BattleEffect)System.Activator.CreateInstance(type, applier);
        if (!effect.ParseParams())
            return null;

        return effect;
    }
}
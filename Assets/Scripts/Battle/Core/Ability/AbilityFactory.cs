

using System;
using System.Collections.Generic;

public enum AbilityType
{
    None = 0,
    TemplateAbility,                            // 模板类技能
    PermanentAttribute,
    AttributeMod,
    AttributePercentMod,
}

public class AbilityFactory
{
    private static Dictionary<AbilityType, System.Type> mAbilityClassMap = new Dictionary<AbilityType, System.Type>()
    {
        { AbilityType.TemplateAbility, typeof(AbilityTemplate) },
        { AbilityType.PermanentAttribute, typeof(AbilityPermanentAttribute) },
    };

    public static AbilityBase CreateAbility(int abiID)
    {
        var data = AbilityTable.Find(abiID);
        if (data == null)
            return null;
        
        if (data.mType == AbilityType.TemplateAbility)
        {
            return CreateTemplateAbility(data);
        }
        else
        {
            if (!mAbilityClassMap.TryGetValue(data.mType, out var type))
                return null;

            AbilityBase ability = (AbilityBase)System.Activator.CreateInstance(type);
            ability.Data = data;
            
            return ability;
        }
    }

    public static AbilityTemplate CreateTemplateAbility(int abiID)
    {
        var data = AbilityTable.Find(abiID);
        if (data == null)
            return null;

        return CreateTemplateAbility(data);
    }

    public static AbilityTemplate CreateTemplateAbility(AbilityTable.Data data)
    {
        var templateData = AbilityTemplateTable.Find(data.mKind);
        if (templateData == null)
            return null;

        var triggerData = templateData.mTriggerData;
        var trigger = AbilityTrigger.Create((AbilityTrigger.Type)triggerData.mType);
        if (trigger == null || trigger.Setup(triggerData.mParams))
            return null;
        
        var selectorData = templateData.mSelectorData;
        var selector = AbilityTargetSelector.Create((AbilityTargetSelector.Type)selectorData.mType);
        if (selector == null || selector.Setup(selectorData.mParams))
            return null;

        var effectDataList = templateData.mEffectApplyDataList;
        var effectAppliers = new List<AbilityEffectApplier>();
        
        foreach (var effectData in effectDataList)
        {
            var effectApplier = AbilityEffectApplier.Create((AbilityEffectApplier.Type)effectData.mType);
            if (effectApplier == null || effectApplier.Setup(effectData.mParams))
                return null;
            
            effectAppliers.Add(effectApplier);
        }

        var abi = new AbilityTemplate(trigger, selector, effectAppliers)
        {
            Data = data
        };

        return abi;
    }
}
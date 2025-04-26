

using UnityEditor;
using UnityEngine;

public class BattleHelper
{
    protected static DealDamageCalc mReusableDealDamageCalc = new DealDamageCalc();
    public static DealDamageCalc GetReusableDealDamageCalc(BattleUnit instigator)
    {
        //todo : fill calc info
        mReusableDealDamageCalc.Reset();
        
        mReusableDealDamageCalc.mInstigator = instigator;
        
        return mReusableDealDamageCalc;
    }
    
    protected static TakeDamageCalc mReusableTakeDamageCalc = new TakeDamageCalc();
    public static TakeDamageCalc ReusableTakeDamageCalc => mReusableTakeDamageCalc;

    public static bool IsPositiveEffect(BattleEffectSpec spec)
    {
        return false;
    }
    
    public static bool IsNegativeEffect(BattleEffectSpec spec)
    {
        return !IsPositiveEffect(spec);
    }

    public static float MergerEffectValue(EffectType type, float a, float b)
    {
        //todo: expand merge function
        return a + b;
    }

    public static EffectType ToEffectType(AttributeType type, bool bPositive)
    {
        switch (type)
        {
            case AttributeType.Attack:
                return bPositive ? EffectType.AttackUp : EffectType.AttackDown;
            
            case AttributeType.Def:
                return bPositive ? EffectType.DefUp : EffectType.DefDown;
            
            case AttributeType.Psy:
                return bPositive ? EffectType.PsyUp : EffectType.PsyDown;
            
            case AttributeType.San:
                return bPositive ? EffectType.SanUp : EffectType.SanDown;
            
            case AttributeType.MaxHp:
                return bPositive ? EffectType.MaxHpUp : EffectType.MaxHpDown;
            
            default:
                break;
        }

        return EffectType.None;
    }
    
    public static AttributeType ToAttrType(EffectType type)
    {
        switch (type)
        {
            case EffectType.AttackUp:
            case EffectType.AttackDown:
                return AttributeType.Attack;
            
            case EffectType.DefUp:
            case EffectType.DefDown:
                return AttributeType.Def;
            
            case EffectType.PsyUp:
            case EffectType.PsyDown:
                return AttributeType.Psy;
            
            case EffectType.SanUp:
            case EffectType.SanDown:
                return AttributeType.San;
            
            case EffectType.MaxHpUp:
            case EffectType.MaxHpDown:
                return AttributeType.MaxHp;
        }

        return AttributeType.None;
    }
}
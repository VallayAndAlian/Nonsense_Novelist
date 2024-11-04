

using UnityEditor;
using UnityEngine;

public class BattleHelper
{
    protected static DealDamageCalc mReusableDealDamageCalc = new DealDamageCalc();
    public static DealDamageCalc GetReusableDealDamageCalc(AbstractCharacter instigator)
    {
        //todo : fill calc info
        
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
}
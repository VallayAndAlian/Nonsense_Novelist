

public class BattleStatics
{
    protected static DealDamageCalc mReusableDealDamageCalc = new DealDamageCalc();
    public static DealDamageCalc GetReusableDealDamageCalc(AbstractCharacter instigator)
    {
        //todo : fill calc info
        
        return mReusableDealDamageCalc;
    }
    
    protected static TakeDamageCalc mReusableTakeDamageCalc = new TakeDamageCalc();
    public static TakeDamageCalc ReusableTakeDamageCalc => mReusableTakeDamageCalc;
}
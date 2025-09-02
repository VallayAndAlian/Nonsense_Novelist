

public class BEDamage : BattleEffect
{
    public Formula mFixDamage = new Formula("FixDamage");

    protected override void AddParams()
    {
        mParams.Add(mFixDamage);
    }

    public override void Execute()
    {
        DealDamageCalc dmg = BattleHelper.GetReusableDealDamageCalc(mInstigator);
        dmg.mTarget = mTarget;
        dmg.mAbility = mAbility;
        dmg.mMinAttack = mFixDamage.EvaluateInt(null);
        dmg.mMaxAttack = mFixDamage.EvaluateInt(null);
        dmg.mMagic = true;
        dmg.mFlag |= DealDamageFlag.Fixed;
        
        DamageHelper.ProcessDamage(dmg);
    }
}
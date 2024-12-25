

using System.Collections.Generic;
public class ServantsAgent : UnitComponent
{
    protected List<BattleUnit> mServants = new List<BattleUnit>();
    public List<BattleUnit> Servants => mServants;
    public BattleUnit RegisterServants(int servantID)
    {
        UnitInstance servantInstance = new UnitInstance()
        {
            mKind = servantID,
            mCamp = mOwner.Camp
        };
        
        BattleUnit newServant = mOwner.Battle.ObjectFactory.CreateBattleUnit(servantInstance);
        if (newServant == null)
            return null;

        if (!IsVaildServants(newServant)) 
            return null;
        
        while (mServants.Count > 2)
        {
            RemoveServants();
        }
        
        newServant.ServantOwner = mOwner;
        
        Servants.Add(newServant);
        return newServant;
    }

    public void RemoveServants(int index = 0)
    {
        if (Servants.Count <= index) 
            return;

        Servants[index].Die(new DamageReport());
        Servants.RemoveAt(index);
    }

    public bool RemoveServants(BattleUnit servant)
    {
        if (!Servants.Contains(servant)) 
            return false;

        servant.Die(new DamageReport());
        Servants.Remove(servant);
        return true;
    }

    protected bool IsVaildServants(BattleUnit servant)
    {
        if (servant.Data.mInitType != BattleUnitType.Servant) 
            return false;
        
        return true;
    }

}
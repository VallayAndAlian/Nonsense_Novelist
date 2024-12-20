﻿

using System.Collections.Generic;
public class ServantsAgent : UnitComponent
{
    protected List<BattleUnit> mServants = new List<BattleUnit>();
    public List<BattleUnit> Servants => mServants;
    public BattleUnit RegisterServants(int servantID)
    {
        BattleUnit newServant = mOwner.Battle.mObjectFactory.CreateBattleUnit(servantID);

        if (!IsVaildServants(newServant)) 
            return null;

        if (newServant == null)
            return null;
        
        while (mServants.Count > 2)
        {
            RemoveServants();
        }
        
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
        if (servant.Daata.mInitType != BattleUnitType.Servant) 
            return false;
        
        return true;
    }

}

using System.Collections.Generic;

public enum BattleObjectType
{
    none=0,
    BattleUnit=1,

}
public enum BattleUnitType
{
    Character = 0,  // 书本角色
    Servant = 1, // 角色随从
    Monster = 2, // 怪物

}

public class BattleObjectFactory
{
    private static Dictionary<BattleObjectType, System.Type> mBattleObjectClassMap = new Dictionary<BattleObjectType, System.Type>()
    {
        { BattleObjectType.BattleUnit, typeof(BattleUnit) }
    };
    
    public static BattleUnit CreateBattleUnit(int ID)
    {
        var data = BattleUnitTable.Find(ID);
        if (data == null)
            return null;

        return CreateBattleUnit(data);
    }

    public static BattleUnit CreateBattleUnit(BattleUnitTable.Data data)
    {
        var unitData = BattleUnitTable.Find(data.mKind);
        if (unitData == null)
            return null;

        var unit =new BattleUnit(unitData);
        if (unit == null)
            return null;

        return unit;

    }
}

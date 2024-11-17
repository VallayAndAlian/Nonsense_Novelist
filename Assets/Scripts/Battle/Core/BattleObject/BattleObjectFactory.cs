
using System.Collections.Generic;

public enum BattleObjectType
{
    none=0,
    BattleUnit=1,

}
public enum BattleUnitType
{
    Character = 0,  // �鱾��ɫ
    Servant = 1, // ��ɫ���
    Monster = 2, // ����

}

public class BattleObjectFactory : BattleModule
{
    private static Dictionary<BattleObjectType, System.Type> mBattleObjectClassMap = new Dictionary<BattleObjectType, System.Type>()
    {
        { BattleObjectType.BattleUnit, typeof(BattleUnit) }
    };
    
    public BattleUnit CreateBattleUnit(int ID)
    {
        var data = BattleUnitTable.Find(ID);
        if (data == null)
            return null;

        return CreateBattleUnit(data);
    }

    public BattleUnit CreateBattleUnit(BattleUnitTable.Data data)
    {
        var unitData = BattleUnitTable.Find(data.mKind);
        if (unitData == null)
            return null;

        var unit =new BattleUnit(unitData);
        if (unit == null)
            return null;

        Battle.mObjectManager.RegisterUnit(unit);

        return unit;

    }
}


using System.Collections.Generic;
using System.Diagnostics;

public enum BattleObjectType
{
    none=0,
    BattleUnit=1,
    PinBall=2

}
public enum BattleUnitType
{
    Character = 0,  
    Servant = 1, 
    Monster = 2,

}

public class BattleObjectFactory : BattleModule
{
    private static Dictionary<BattleObjectType, System.Type> mBattleObjectClassMap = new Dictionary<BattleObjectType, System.Type>()
    {
        { BattleObjectType.BattleUnit, typeof(BattleUnit) },
        { BattleObjectType.PinBall, typeof(PinBall) }
    };
    
#region  pinball
    public PinBall CreatePinBall(WordTable.Data data)
    {
        PinBall ball=null;
        switch(data.mShootType)
        {
            case ShootType.None:
            {
                ball =new PinBall_none(data);
            }break;
            case ShootType.Split:
            {
                ball =new PinBall_Split(data);
            }break;
            case ShootType.Alpha:
            {
                ball =new PinBall_Alpha(data);
            }break;
            case ShootType.spread:
            {
                ball =new PinBall_Spread(data); 
            }break;
            case ShootType.Activate:
            {
                ball =new PinBall_Activate(data);    
            }break;
              case ShootType.Small:
            {
                ball =new PinBall_Small(data);
            }break;
              case ShootType.Big:
            {
                ball =new PinBall_Big(data);
            }break;
              case ShootType.Add:
            {
                ball =new PinBall_Add(data);
            }break;
              case ShootType.Mirror:
            {
                ball =new PinBall_Mirror(data);
            }break;
              case ShootType.Copy:
            {
                ball =new PinBall_Copy(data);
            }break;
              case ShootType.Dead:
            {
                ball =new PinBall_Dead(data);
            }break;
            case ShootType.Expect:
            {
                ball =new PinBall_Expect(data);
            }break;
            case ShootType.ReTrigger:
            {
                ball =new PinBall_ReTrigger(data);
            }break;
            case ShootType.SameChara:
            {
                ball =new PinBall_SameChara(data);
            }break;
            case ShootType.Servants:
            {
                ball =new PinBall_Servants(data);
            }break;
            case ShootType.Start:
            {
                ball =new PinBall_Start(data);
            }break;
        }
        if (ball == null)
            return null;

        Battle.mObjectManager.RegisterObject(ball);
        return ball;
    
    }
#endregion

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

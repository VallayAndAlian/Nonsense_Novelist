using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.UI.CanvasScaler;

public class BattleUnitManager : BattleModule
{
    private List<BattleUnit> mBattleUnitList=new List<BattleUnit>();
    public void AddCharacter(BattleUnit unit)
    {
        if (!mBattleUnitList.Contains(unit))
        {
            mBattleUnitList.Add(unit);
        }
        else
        {
            return;
        }
    }
    public void RemoveCharacter(BattleUnit unit)
    {
        mBattleUnitList.Remove(unit);
    }
    public List<BattleUnit> GetUnitReadyToLevelUp()
    {
        return mBattleUnitList;
    }
}
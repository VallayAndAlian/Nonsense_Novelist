
// battle visual function

using UnityEngine;

public static class BVHelper
{
    public static BattleBase GetBattle()
    {
        return BattleRunner.Instance != null ? BattleRunner.Instance.Battle : null;
    }

    public static BattleUnit GetUnitAtPosition(Vector3 pos)
    {
        var battle = GetBattle();
        if (battle == null)
            return null;
        
        BattleUnit rst = null;
        float minDisSqr = float.MaxValue;
        foreach (var unit in battle.ObjectManager.Units.Values)
        {
            if (!unit.IsAlive)
                continue;

            float disSqr = (unit.UnitView.transform.position - pos).sqrMagnitude;
            if (minDisSqr > disSqr)
            {
                rst = unit;
                minDisSqr = disSqr;
            }
        }
        
        return rst;
    }
}
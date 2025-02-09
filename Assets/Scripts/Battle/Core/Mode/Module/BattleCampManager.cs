
using System.Collections.Generic;

public class BattleCampManager : BattleModule
{
    protected Dictionary<CampEnum, List<BattleUnit>> mCampUnit = new Dictionary<CampEnum, List<BattleUnit>>();

    public override void Init()
    {
        mCampUnit.Add(CampEnum.left, new List<BattleUnit>());
        mCampUnit.Add(CampEnum.right, new List<BattleUnit>());
        mCampUnit.Add(CampEnum.stranger, new List<BattleUnit>());
    }

    public override void Begin()
    {
        EventManager.Subscribe<BattleUnit>(EventEnum.UnitSpawn, OnUnitSpawn);
        EventManager.Subscribe<BattleUnit>(EventEnum.UnitDie, OnUnitDie);
    }

    public void OnUnitSpawn(BattleUnit unit)
    {
        if (mCampUnit.TryGetValue(unit.Camp, out var list))
        {
            list.Add(unit);
        }
    }
    
    public void OnUnitDie(BattleUnit unit)
    {
        if (mCampUnit.TryGetValue(unit.Camp, out var list))
        {
            list.Remove(unit);
        }
    }

    public List<BattleUnit> GetAllies(BattleUnit unit)
    {
        List<BattleUnit> allies = new List<BattleUnit>();
        if (mCampUnit.TryGetValue(unit.Camp, out var list))
        {
            allies.AddRange(list);
            allies.Remove(unit);
        }

        return allies;
    }
    
    public List<BattleUnit> GetEnemies(BattleUnit unit)
    {
        List<BattleUnit> enemies = new List<BattleUnit>();
        foreach (var iter in mCampUnit)
        {
            if (iter.Key != unit.Camp)
            {
                enemies.AddRange(iter.Value);
            }
        }

        return enemies;
    }
}
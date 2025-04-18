﻿
using System.Collections.Generic;

public class BattleCampManager : BattleModule
{
    protected Dictionary<BattleCamp, List<BattleUnit>> mCampUnit = new Dictionary<BattleCamp, List<BattleUnit>>();

    public override void Init()
    {
        mCampUnit.Add(BattleCamp.Camp1, new List<BattleUnit>());
        mCampUnit.Add(BattleCamp.Camp2, new List<BattleUnit>());
        mCampUnit.Add(BattleCamp.Boss, new List<BattleUnit>());
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
    public List<BattleUnit> GetCampMember(BattleCamp camp)
    {
        return mCampUnit[camp];
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
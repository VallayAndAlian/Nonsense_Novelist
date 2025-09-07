﻿
using System.Collections.Generic;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEditor.Progress;

public class BattleCampManager : BattleModule
{
    protected Dictionary<BattleCamp, List<BattleUnit>> mCampUnit = new Dictionary<BattleCamp, List<BattleUnit>>();
    protected Dictionary<BattleCamp ,float > mCampHp= new Dictionary<BattleCamp ,float >();
    protected float mInitCampHp=>BattleConfig.mData.camp.initCampHp;
    public override void Init()
    {
        mCampUnit.Add(BattleCamp.Camp1, new List<BattleUnit>());
        mCampUnit.Add(BattleCamp.Camp2, new List<BattleUnit>());
        mCampUnit.Add(BattleCamp.Boss, new List<BattleUnit>());
        mCampHp.Add(BattleCamp.Camp1, mInitCampHp);
        mCampHp.Add(BattleCamp.Camp2, mInitCampHp);
    }

    public override void Begin()
    {
        EventManager.Subscribe<BattleUnit>(EventEnum.UnitSpawn, OnUnitSpawn);
        EventManager.Subscribe<BattleUnit>(EventEnum.UnitDie, OnUnitDie);
        EventManager.Subscribe<BattleUnit>(EventEnum.UnitRemove, OnUnitRemove);
        EventManager.Subscribe<BattleUnit, BattleCamp>(EventEnum.UnitChangeCamp, OnUnitChangeCamp);
    }

    public void OnUnitSpawn(BattleUnit unit)
    {
        if (mCampUnit.TryGetValue(unit.Camp, out var list))
        {
            if (!list.Contains(unit))
            {
                list.Add(unit);
            }
        }
    }
    
    public void OnUnitDie(BattleUnit unit)
    {
        // if (mCampUnit.TryGetValue(unit.Camp, out var list))
        // {
        //     list.Remove(unit);
        // }
    }
    
    public void OnUnitRemove(BattleUnit unit)
    {
        if (mCampUnit.TryGetValue(unit.Camp, out var list))
        {
            list.Remove(unit);
        }
    }
    
    public void OnUnitChangeCamp(BattleUnit unit, BattleCamp oldCamp)
    {
        {
            if (mCampUnit.TryGetValue(oldCamp, out var list))
            {
                list.Remove(unit);
            }
        }
        
        {
            if (mCampUnit.TryGetValue(unit.Camp, out var list))
            {
                if (!list.Contains(unit))
                {
                    list.Add(unit);
                }
            }
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
    public List<BattleUnit> GetAlliesExceptServant(BattleUnit unit)
    {
        List<BattleUnit> allies = new List<BattleUnit>();
        if (mCampUnit.TryGetValue(unit.Camp, out var list))
        {
            foreach (var item in list)
            {
                if (item.Data.mInitType == BattleUnitType.Character)
                {
                    allies.Add(item);
                }
            }
            allies.Remove(unit);
        }

        return allies;
    }
    public List<BattleUnit> GetEnemiesExceptServant(BattleUnit unit)
    {
        if (!Battle.BattlePhase.CampEnemies.TryGetValue(unit.Camp, out var enemyCamp))
            return new List<BattleUnit>();

        List<BattleUnit> enemies = new List<BattleUnit>();
        foreach (var iter in mCampUnit)
        {
            if (iter.Key != unit.Camp && enemyCamp.Contains(iter.Key))
            {
                var members = GetCampMember(iter.Key);
                foreach(var member in members)
                {
                    if(member.Data.mInitType == BattleUnitType.Character)
                    {
                        enemies.AddRange(iter.Value);
                        break;
                    }
                    else
                    {
                        return new List<BattleUnit>();
                    }
                }
            }
        }

        return enemies;
    }
    public List<BattleUnit> GetEnemies(BattleUnit unit)
    {
        if (!Battle.BattlePhase.CampEnemies.TryGetValue(unit.Camp, out var enemyCamp))
            return new List<BattleUnit>();
        
        List<BattleUnit> enemies = new List<BattleUnit>();
        foreach (var iter in mCampUnit)
        {
            if (iter.Key != unit.Camp && enemyCamp.Contains(iter.Key))
            {
                enemies.AddRange(iter.Value);
            }
        }

        return enemies;
    }

    public void UpdateCampHp(BattleCamp camp, float hp)
    {
        mCampHp[camp] -= hp;
        if (mCampHp[camp] > 0)
        {
            EventManager.Invoke(EventEnum.ChangeCampHp, camp, mCampHp[camp] / mInitCampHp);
        }
        else
        {
            EventManager.Invoke(EventEnum.BattleEnd, camp);//失败阵营
        }
    }
}
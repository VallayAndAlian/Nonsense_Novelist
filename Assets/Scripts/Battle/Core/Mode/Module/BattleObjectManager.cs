﻿
using System.Collections.Generic;
using System.Linq;

public class BattleObjectManager : BattleModule
{
    protected int mGenID = 0;
    protected Dictionary<int, BattleObject> mObjects = new Dictionary<int, BattleObject>();
    protected Dictionary<int, BattleUnit> mUnits = new Dictionary<int, BattleUnit>();

    protected List<int> mRemovedIDs = new List<int>();

    protected bool mStarted = false;

    public T Find<T>(int id) where T : BattleObject
    {
        if (mObjects.TryGetValue(id, out var obj))
        {
            return obj as T;
        }

        return null;
    }

    public bool RegisterObject(BattleObject obj)
    {
        if (obj.IsRegistered)
            return false;

        obj.ID = ++mGenID;
        obj.Battle = Battle;
        obj.IsRegistered = true;
        
        obj.Start();
        
        mObjects.TryAdd(obj.ID, obj);
        
        return true;
    }


    public bool RegisterUnit(BattleUnit unit)
    {
        if (!unit.IsRegistered)
            return false;
            
        unit.Init();
        
        RegisterObject(unit);
        
        mUnits.Add(unit.ID, unit);
        
        return true;
    }

    public void RemoveObject(BattleObject obj)
    {
        if (obj != null)
        {
            obj.MarkPendingKill();
        }
    }

    public void ClearTrashObjects()
    {
        {
            foreach (var objIt in mObjects.Values)
            {
                if (objIt.IsPendingKill)
                {
                    mRemovedIDs.Add(objIt.ID);
                }
            }

            foreach (var objId in mRemovedIDs)
            {
                mObjects.Remove(objId);
            }

            mRemovedIDs.Clear();
        }

        {
            foreach (var objIt in mUnits.Values)
            {
                if (objIt.IsPendingKill)
                {
                    mRemovedIDs.Add(objIt.ID);
                }
            }

            foreach (var objId in mRemovedIDs)
            {
                mUnits.Remove(objId);
            }
        }
        mRemovedIDs.Clear();
    }

    public override void Update(float deltaSec)
    {
        ClearTrashObjects();
        
        foreach (var objIt in mObjects.Values.Where(objIt => objIt.IsTickEnable))
        {
            objIt.Update(deltaSec);
        }
    }

    public override void LateUpdate(float deltaSec)
    {
        foreach (var objIt in mObjects.Values.Where(objIt => objIt.IsTickEnable))
        {
            objIt.LateUpdate(deltaSec);
        }
    }
}
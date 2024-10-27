
using System.Collections.Generic;
using System.Linq;

public class BattleObjectManage : BattleModule
{
    protected int mGenID = 0;
    protected Dictionary<int, BattleObject> mObjects = new Dictionary<int, BattleObject>();
    protected Dictionary<int, BattleUnit> mUnits = new Dictionary<int, BattleUnit>();

    protected List<int> mRemovedObjects = new List<int>();
    protected List<int> mRemovedUnits = new List<int>();

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
        obj.IsRegistered = true;
        
        obj.Init();
        obj.OnRegistered();
        
        mObjects.TryAdd(obj.ID, obj);

        return true;
    }


    public bool RegisterUnit(BattleUnit unit)
    {
        if (!RegisterObject(unit))
            return false;

        mUnits.Add(unit.ID, unit);
        
        if (Battle.mState == BattleBase.BattleState.Inprogress)
        {
            unit.Start();
        }

        return true;
    }

    public void RemoveObject(BattleObject obj)
    {
        if (obj != null)
        {
            obj.IsTickEnable = false;
            mRemovedObjects.Add(obj.ID);

            if (obj is BattleUnit unit)
            {
                mRemovedUnits.Add(obj.ID);
            }
        }
    }

    public void ClearTrashObjects()
    {
        foreach (var objId in mRemovedObjects)
        {
            mObjects.Remove(objId);
        }
        
        foreach (var objId in mRemovedUnits)
        {
            mUnits.Remove(objId);
        }
        
        mRemovedObjects.Clear();
        mRemovedUnits.Clear();
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

using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleStage : BattleModule
{
    #region UnitSlot
    
    private List<UnitSlot> mUnitSlots = new List<UnitSlot>();
    private int mGenID = 0;

    public static bool IsValidSlot(UnitSlot slot)
    {
        return slot != null && slot.ID > 0;
    }
    
    public override void Init()
    {
        mUnitSlots.Clear();
        
        var slots = GameObject.FindObjectsByType<UnitSlot>(FindObjectsSortMode.InstanceID);
        foreach (var slot in slots)
        {
            AddSlot(slot);
        }
    }

    public void AddSlot(UnitSlot slot)
    {
        slot.ID = ++mGenID;
        mUnitSlots.Add(slot);
    }

    public void RemoveSlot(UnitSlot slot)
    {
        if (slot == null)
            return;

        mUnitSlots.Remove(slot);
    }

    public UnitSlot FindSlot(UnitSlotType type)
    {
        foreach (var slot in mUnitSlots)
        {
            if (!IsValidSlot(slot))
                continue;
            
            if (!slot.IsOccupied && slot.SlotType == type)
            {
                return slot;
            }
        }

        return null;
    }

    public UnitSlot FindClosestSlot(UnitSlotType typeFlags, Vector2 pos, float range)
    {
        float misDisSqr = float.MaxValue;
        UnitSlot closestSlot = null;

        foreach (var slot in mUnitSlots)
        {
            if (!IsValidSlot(slot))
                continue;
            
            if ((slot.SlotType & typeFlags) == 0 || slot.IsOccupied)
                continue;

            float disSqr = (slot.Pos - pos).sqrMagnitude;
            if (disSqr > range * range)
                continue;

            if (closestSlot == null || misDisSqr > disSqr)
            {
                closestSlot = slot;
                misDisSqr = disSqr;
            }
        }

        return closestSlot;
    }

    public UnitSlot GetSlot(int id)
    {
        foreach (var slot in mUnitSlots)
        {
            if (IsValidSlot(slot) && slot.ID == id)
            {
                return slot;
            }
        }

        return null;
    }
    #endregion
}
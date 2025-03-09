using System;
using UnityEngine;

[System.Flags]
public enum UnitSlotType
{
    None = 0,
    FrontSeat = 1 << 0,
    BackSeat = 1 << 1,
    Servant = 1 << 2,
    Boss = 1 << 3,
}

public class UnitSlot : MonoBehaviour
{
    [SerializeField] 
    private UnitSlotType mSlotType = UnitSlotType.None;
    public UnitSlotType SlotType => mSlotType;
    
    [SerializeField] 
    private BattleCamp mDefaultCamp = BattleCamp.None;
    public BattleCamp SpawnCamp 
    {
        get
        {
            if (SlotType == UnitSlotType.BackSeat || SlotType == UnitSlotType.FrontSeat)
            {
                return mDefaultCamp;
            }

            if (SlotType == UnitSlotType.Boss)
            {
                return BattleCamp.Neutral;
            }
            
            return BattleCamp.None;
        }
    }

    private BattleUnit mUnit = null;
    public BattleUnit Unit => mUnit;

    public Vector2 Pos => transform.position;

    protected int mID = -1;
    public int ID
    {
        get => mID;
        set => mID = value;
    }

    public bool IsOccupied => Unit != null;

    public void OccupiedBy(BattleUnit unit)
    {
        if (IsOccupied)
        {
            Debug.LogError("this slot has Occupied already");
            return;
        }

        mUnit = unit;
        mUnit.Slot = this;
    }

    public void Remove()
    {
        mUnit.Slot = null;
        mUnit = null;
    }
}
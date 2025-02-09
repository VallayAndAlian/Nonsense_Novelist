using UnityEngine;

public enum UnitSlotType
{
    None = 0,
    FrontSeat,
    BackSeat,
    Servant,
    Boss,
}

public class UnitSlot : MonoBehaviour
{
    [SerializeField] 
    private UnitSlotType mSlotType = UnitSlotType.None;
    public UnitSlotType SlotType => mSlotType;

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

        mUnit.Slot = this;
        mUnit = unit;
    }

    public void Remove()
    {
        mUnit.Slot = null;
        mUnit = null;
    }
}
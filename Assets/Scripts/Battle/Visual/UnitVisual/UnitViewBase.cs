using System.Collections.Generic;
using UnityEngine;

public class UnitViewBase : MonoBehaviour
{
    protected BattleUnit mRole = null;
    public BattleUnit Role => mRole;
    
    protected BattleUnitSO mAsset = null;
    public BattleUnitSO Asset => mAsset;
    
    protected Transform mRoot = null;
    public Transform Root => mRoot;

    protected UnitModelLayout mModelLayout = null;
    public UnitModelLayout ModelLayout => mModelLayout;

    protected List<UnitSlot> mServantSlots = new List<UnitSlot>();

    public bool IsCompatibleType(BattleUnit role)
    {
        return true;
    }

    public void Setup(BattleUnit role, BattleUnitSO asset)
    {
        mRole = role;
        mAsset = asset;
        
        mRole.UnitView = this;
        
        mModelLayout = GetComponent<UnitModelLayout>();
        if (mModelLayout == null)
            mModelLayout = gameObject.AddComponent<UnitModelLayout>();
        
        mModelLayout.Setup(this);

        mRoot = transform.parent;

        var slots = mRoot.GetComponentsInChildren<UnitSlot>();
        foreach (var slot in slots)
        {
            mRole.Battle.Stage.AddSlot(slot);
            mServantSlots.Add(slot);
        }

        if (mRole.Slot)
        {
            mRoot.parent = mRole.Slot.transform;
        }
        else if (mRole.ServantOwner != null)
        {
            mRole.ServantOwner.UnitView.AddServant(this);
        }
        
        GetComponent<SpriteRenderer>().sprite = mAsset.sprite;
    }

    public void AddServant(UnitViewBase unitView)
    {
        if (unitView != null && unitView.Role != null)
        {
            foreach (var slot in mServantSlots)
            {
                if (!slot.IsOccupied)
                {
                    slot.OccupiedBy(unitView.Role);
                    unitView.Root.parent = slot.transform;
                }
            }
        }
    }

    public void OnStartEmit(EmitMeta meta)
    {
        // create projectile
    }
}
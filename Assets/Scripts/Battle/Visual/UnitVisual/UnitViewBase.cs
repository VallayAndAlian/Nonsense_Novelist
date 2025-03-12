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
        if (mRole.Slot)
        {
            mRoot.parent = mRole.Slot.transform;
            mRoot.localPosition = Vector3.zero;
        }

        var slots = mRoot.GetComponentsInChildren<UnitSlot>();
        foreach (var slot in slots)
        {
            mRole.Battle.Stage.AddSlot(slot);
            mServantSlots.Add(slot);
        }
        
        foreach (var servant in mRole.ServantsAgent.Servants)
        {
            AddServant(servant.UnitView);
        }
        
        GetComponent<SpriteRenderer>().sprite = mAsset.sprite;
    }

    public void AddServant(UnitViewBase unitView)
    {
        if (mServantSlots.Count == 0)
            return;
        
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
        var projObj = Object.Instantiate(mAsset.weaponProj);
        var proj = projObj.GetComponent<NnProjectile>();
        
        proj.Emit(this, meta);
    }
}
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

    public Vector3 Pos => mRoot.transform.position;
    public Vector3 CenterPos => Pos;//todo: replace it with a real center pos

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

        if (mRole.ServantsAgent != null)
        {
            foreach (var servant in mRole.ServantsAgent.Servants)
            {
                AddServant(servant.UnitView);
            }
        }

        GetComponent<SpriteRenderer>().sprite = mAsset.sprite;
    }

    public void AddServant(UnitViewBase unitView)
    {
        if (mServantSlots.Count == 0)
            return;
        
        if (unitView && unitView.Role != null)
        {
            foreach (var slot in mServantSlots)
            {
                if (!slot.IsOccupied)
                {
                    slot.OccupiedBy(unitView.Role);
                    unitView.Root.parent = slot.transform;
                    unitView.Root.localPosition = Vector3.zero;
                }
            }
        }
    }

    public void OnUnitDie()
    {
        GetComponent<SpriteRenderer>().enabled=false;
        mRoot.gameObject.SetActive(false);
        // Destroy(mRoot.gameObject);
    }

    public void OnApplyEffect(BattleEffect be)
    {
        
    }
    
    public void OnRemoveEffect(BattleEffect be)
    {
        
    }
}
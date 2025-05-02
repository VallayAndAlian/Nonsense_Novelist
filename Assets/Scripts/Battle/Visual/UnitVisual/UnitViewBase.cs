﻿using System;
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
    
    [Header("动画类型")] 
    public UnitAnimType mAnimType = UnitAnimType.Anim;

    protected UnitModelLayout mModelLayout = null;
    public UnitModelLayout ModelLayout => mModelLayout;

    protected List<UnitSlot> mServantSlots = new List<UnitSlot>();

    public Vector3 Pos => mRoot.transform.position;
    public Vector3 CenterPos => Pos;//todo: replace it with a real center pos

    public class EffectFxCache
    {
        public EffectType mType;
        public int mCount;
        public List<GameObject> mPlayedFx = new List<GameObject>();
    }

    protected List<EffectFxCache> mEffectFxCache = new List<EffectFxCache>();

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
            mRoot.localPosition = Vector3.Scale(-mRoot.Find("seat").localPosition, mRoot.localScale);
        }

        var slots = mRoot.GetComponentsInChildren<UnitSlot>();
        foreach (var slot in slots)
        {
            slot.ServantOwner = mRole;
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

        var spriteComp = GetComponent<SpriteRenderer>();
        if (spriteComp != null)
        {
            spriteComp.sprite = mAsset.sprite;
        }
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
        Destroy(mRoot.gameObject, 1.0f);
    }

    public void OnApplyEffect(BattleEffect be)
    {
        var effectAsset = AssetManager.GetEffectAsset();
        if (effectAsset == null)
            return;

        var fxData = effectAsset.GetFxList(be.mType);
        if (fxData == null || fxData.mFxList.Count > 0)
            return;
        
        bool bHasScriptFx = false;
        var fxList = fxData.mFxList;
        
        var getSocket = new Func<string, Transform>((string socketName) =>
        {
            var soc = mRoot.Find(socketName);
            return soc == null ? mRoot : soc;
        });
        
        foreach (var it in fxList)
        {
            if (it.mPlayType == EffectFxPlayType.Instant)
            {
                GameObject.Instantiate(it.mFxObj, getSocket(it.mSocketName));
            }
            else if (it.mPlayType == EffectFxPlayType.Script)
            {
                bHasScriptFx = true;
            }
        }
        
        if (!bHasScriptFx)
            return;
        
        foreach (var it in mEffectFxCache)
        {
            if (be.mType == it.mType)
            {
                ++it.mCount;
                return;
            }
        }
        
        EffectFxCache cache = new EffectFxCache();
        cache.mType = be.mType;
        cache.mCount = 1;
        
        foreach (var it in fxList)
        {
            if (it.mPlayType == EffectFxPlayType.Script)
            {
                cache.mPlayedFx.Add(GameObject.Instantiate(it.mFxObj, getSocket(it.mSocketName)));
            }
        }
        
        mEffectFxCache.Add(cache);
    }
    
    public void OnRemoveEffect(BattleEffect be)
    {
        foreach (var it in mEffectFxCache)
        {
            if (be.mType == it.mType)
            {
                --it.mCount;
                if (it.mCount <= 0)
                {
                    foreach (var fx in it.mPlayedFx)
                    {
                        Destroy(fx);
                    }
                    
                    mEffectFxCache.Remove(it);
                }
                break;
            }
        }
    }
}
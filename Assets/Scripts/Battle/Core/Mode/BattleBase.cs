using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleBase : MonoBehaviour
{
    public enum BattleState
    {
        None = 0,
        Inprogress,
        End,
    }
    
    public BattleState mState  { get; set; }
    
    protected List<BattleModule> mModules = new List<BattleModule>();

    public BattleClock mClock = null;
    public BattleStage mStage = null;
    public BattleGameState mGameState = null;
    public BattleObjectManage mObjectManager = null;
    
    public float Now => mClock?.ElapsedSec ?? 0;

    public void Init()
    {
        mState = BattleState.None;
        
        AddModules();
        InitModules();
    }

    public void Begin()
    {
        mState = BattleState.Inprogress;
        
        foreach (var module in mModules)
        {
            module.Begin();
        }
        
        foreach (var module in mModules)
        {
            module.PostBegin();
        }
    }

    public void Tick(float deltaSec)
    {
        if (mState != BattleState.Inprogress)
            return;
        
        foreach (var module in mModules)
        {
            module.Update(deltaSec);
        }
        
        foreach (var module in mModules)
        {
            module.LateUpdate(deltaSec);
        }
    }

    public void Close()
    {
        foreach (var module in mModules)
        {
            module.Dispose();
        }
        
        mModules.Clear();
    }
    
    protected virtual void AddModules()
    {
        mClock = new BattleClock();
        RegisterModule(mClock);
        
        mStage = new BattleStage();
        RegisterModule(mStage);
        
        mGameState = new BattleGameState();
        RegisterModule(mGameState);
        
        mObjectManager = new BattleObjectManage();
        RegisterModule(mObjectManager);
    }

    protected void RegisterModule(BattleModule module)
    {
        module.Battle = this;
        mModules.Add(module);
    }

    private void InitModules()
    {
        foreach (var module in mModules)
        {
            module.Init();
        }
    }
}
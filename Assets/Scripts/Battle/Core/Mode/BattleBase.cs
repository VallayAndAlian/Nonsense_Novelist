using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleBase : MonoBehaviour
{

    protected BattleState mState = BattleState.None;
    public BattleState State => mState;
    
    protected List<BattleModule> mModules = new List<BattleModule>();

    protected BattleClock mClock = null;
    protected BattleStage mStage = null;
    protected BattleGameState mGameState = null;
    protected BattleObjectManager mObjectManager = null;
    protected BattleObjectFactory mObjectFactory = null;
    protected BattleCampManager mCampManager = null;
    protected CardDeckManager mCardDeckManager = null;
    protected PinBallLauncher mPinBallLauncher = null;
    
    public BattleClock Clock => mClock;
    public BattleStage Stage => mStage;
    public BattleGameState GameState => mGameState;
    public BattleObjectManager ObjectManager => mObjectManager;
    public BattleObjectFactory ObjectFactory => mObjectFactory;
    public BattleCampManager CampManager => mCampManager;
    public CardDeckManager CardDeckManager => mCardDeckManager;
    public PinBallLauncher PinBallLauncher => mPinBallLauncher;
    
    public float Now => mClock?.ElapsedSec ?? 0;

    public bool IsFinished => mState == BattleState.End;

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
        
        mObjectManager = new BattleObjectManager();
        RegisterModule(mObjectManager);
        
        mObjectFactory = new BattleObjectFactory();
        RegisterModule(mObjectFactory);
        
        mCampManager = new BattleCampManager();
        RegisterModule(mCampManager);

        mCardDeckManager = new CardDeckManager();
        RegisterModule(mCardDeckManager);

        mPinBallLauncher = new PinBallLauncher();
        RegisterModule(mPinBallLauncher);
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
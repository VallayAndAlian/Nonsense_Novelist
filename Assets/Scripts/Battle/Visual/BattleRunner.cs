using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleRunner : MonoBehaviour
{
    public static BattleRunner Instance = null;
    
    protected BattleBase mBattle = null;
    public BattleBase Battle => mBattle;

    protected List<BattleVisualSystemBase> mSystems = new List<BattleVisualSystemBase>();

    private void Start()
    {
        RegisterSingleton();
    }

    private void OnDestroy()
    {
        UnregisterSingleton();
    }

    public void RegisterSingleton()
    {
        Instance = this;
        
        CreateBattle();
    }
    
    public void UnregisterSingleton()
    {
        Instance = null;

        if (mBattle != null)
        {
            mBattle.Close();
            mBattle = null;
        }
    }

    public void CreateBattle()
    {
        mBattle = gameObject.AddComponent<BattleBase>();
    }

    public void LoadBattle()
    {
        // 加载战斗资源
    }

    public void StartBattle()
    {
        if (mBattle.State == BattleState.None)
        {
            mBattle.Init();
            mBattle.Begin();
        }
    }

    public void Update()
    {
        float deltaSec = Time.deltaTime;

        if (!mBattle.IsFinished)
        {
            mBattle.Tick(deltaSec);
        }

        foreach (var system in mSystems)
        {
            system.Tick(deltaSec);
        }
    }
}
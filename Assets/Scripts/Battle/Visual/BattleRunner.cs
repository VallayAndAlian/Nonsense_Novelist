using System;
using UnityEngine;

public class BattleRunner : MonoBehaviour
{
    protected BattleBase mBattle = null;

    private void Start()
    {
        // test battle
        CreateBattle();
        StartBattle();
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
        mBattle.Init();
        mBattle.Begin();
    }

    public void Update()
    {
        mBattle.Tick(Time.deltaTime);
    }
}
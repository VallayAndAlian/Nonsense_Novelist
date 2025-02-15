
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum BattlePhaseType
{
    Rest = 0,//休息回合
    Lve=1,//左对抗怪物
    Rve=2,//右对抗怪物
    Pve=3,//左右一起对抗怪物
    Pvp=4//左右互博
}

public class BattlePhase : BattleModule
{
    protected Dictionary<int, PhaseTable.Data> mPhases = new Dictionary<int, PhaseTable.Data>();
    protected int mStageIndex=-1;       //索引从负1开始，第一个回合也使用EnterNextStage
    protected GamePhase mCurrentPhase;

    public override void Init() 
    {
        //从表格中读取数据，存储在Mphase中
        int temp=0;
        foreach(var data in PhaseTable.DataList)
        {
            if(data.Value.mChapter==1)//读取和当前章节一致的回合。还没写章节相关的东西，这里默认1
            {
                mPhases.Add(temp,data.Value);
                temp++;
            }
        }

        //进入第一个回合
        EnterNextStage();
    }

    public GamePhase RegisterPhase(PhaseTable.Data data)
    {
        GamePhase newPhase;
        switch(data.mType)
        {
            case (int)BattlePhaseType.Lve:
            case (int)BattlePhaseType.Pve:
            case (int)BattlePhaseType.Pvp:
            case (int)BattlePhaseType.Rve:
                {
                   newPhase=NewBattlePhase((BattlePhaseType)data.mType);
                }
                break;
            case (int)BattlePhaseType.Rest:
            default:
                {
                    var restData=new GamePhase_Rest.RestData();
                    restData.mEventCount=3;
                    restData.mEventShowTime=3;
                    newPhase=new GamePhase_Rest(restData);
                }
                break;
        };
        newPhase.Battle = Battle;
        newPhase.IsRegistered = true;
        newPhase.Start();
        
        return newPhase;
    }

    protected GamePhase_Combat NewBattlePhase(BattlePhaseType type)
    {
        List<CampEnum> camps=new List<CampEnum>();
        switch(type)
        {
             case BattlePhaseType.Lve:
                {
                    camps.Add(CampEnum.left); 
                    camps.Add(CampEnum.stranger);
                }
            break;
            case BattlePhaseType.Pve:
                {
                    camps.Add(CampEnum.right); 
                    camps.Add(CampEnum.left);
                    camps.Add(CampEnum.stranger);
                }
            break;
            case BattlePhaseType.Pvp:
                {
                    camps.Add(CampEnum.left); 
                    camps.Add(CampEnum.right);
                }
            break;
            case BattlePhaseType.Rve:
                {
                    camps.Add(CampEnum.right);
                    camps.Add(CampEnum.stranger);
                }
            break;
        }
        return new GamePhase_Combat(camps,0);
    }

    public void EnterNextStage()
    {
         if (mCurrentPhase != null)
        {
            mCurrentPhase.Exit();
            mCurrentPhase=null;
        }

        // 进入下一阶段
        mStageIndex++;
        if (mStageIndex >= mPhases.Count)
        {
            Debug.Log("所有阶段已完成！");
            return;
        }

        // 根据类型创建阶段对象
        var nextData = mPhases[mStageIndex];
        mCurrentPhase = RegisterPhase(nextData);
        mCurrentPhase.Start();

        // 存档
    }

    public override void Update(float deltaSec)
    {
       if(mCurrentPhase!=null)
       {
        mCurrentPhase.Update(deltaSec);
       }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleFloatTextManager : BattleModule
{
    protected Dictionary<int, PhaseTable.Data> mPhases = new Dictionary<int, PhaseTable.Data>();
    protected int mStageIndex=-1;       //索引从负1开始，第一个回合也使用EnterNextStage
    protected GamePhase mCurrentPhase;

    protected BattlePhaseUI_Rest restUI;
    protected GameObject restUIObj;
    protected BattlePhaseUI_Battle battleUI;
    protected GameObject battleUIObj;
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
          InitPhaseUI();
        EnterNextStage();
    }
 protected void InitPhaseUI()
    { 
        if(restUI==null)
        {
            restUI=new BattlePhaseUI_Rest();
            restUI.Battle=Battle;
        }
        if(battleUI==null)
        {
            battleUI=new BattlePhaseUI_Battle();
        }


        Battle.BattleUI.Hide(battleUI);
        Battle.BattleUI.Hide(restUI);
    }

    public void ActivePhaseUI(BattlePhaseType type)
    {
        Battle.BattleUI.Hide(battleUI);
        Battle.BattleUI.Hide(restUI);
        switch(type)
        {
            case  BattlePhaseType.Rest:
            {
                Battle.BattleUI.ShowPanel(restUI);
            }break;
            case  BattlePhaseType.Pve:
            case  BattlePhaseType.Pvp:
            case  BattlePhaseType.Rve:
            case  BattlePhaseType.Lve:
            {
                Battle.BattleUI.ShowPanel(battleUI);
            }break;
        }
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
        List<BattleCamp> camps=new List<BattleCamp>();
        switch(type)
        {
             case BattlePhaseType.Lve:
                {
                    camps.Add(BattleCamp.Camp1); 
                    camps.Add(BattleCamp.Boss);
                }
            break;
            case BattlePhaseType.Pve:
                {
                    camps.Add(BattleCamp.Camp1); 
                    camps.Add(BattleCamp.Camp2);
                    camps.Add(BattleCamp.Boss);
                }
            break;
            case BattlePhaseType.Pvp:
                {
                    camps.Add(BattleCamp.Camp1); 
                    camps.Add(BattleCamp.Camp2);
                }
            break;
            case BattlePhaseType.Rve:
                {
                    camps.Add(BattleCamp.Camp2);
                    camps.Add(BattleCamp.Boss);
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
       
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestUI : BasePanel
{
    protected override void Init()
    {

    }


    protected override void OnClick(string _btnName)
    {
        switch (_btnName)
        {
            case "RestNextStage":
                { 
                    GameMgr.instance.EnterTheStage(GameMgr.instance.stageCount + 1);
                    UIManager.GetInstance().HidePanel("RestUI");
                }
                break;
        }
    }

}

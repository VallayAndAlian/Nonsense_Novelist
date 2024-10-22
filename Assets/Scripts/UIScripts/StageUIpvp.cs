using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 开启pvp战斗阶段时的面板
/// </summary>
public class StageUIpvp : BasePanel
{
    override protected void Init()
    {
        //获取当前A队的角色画像
        SetTeamPic(CampEnum.left);
        SetTeamPic(CampEnum.right);
        
        
    }



    
    override protected void OnClick(string btnName)
    {
        switch (btnName) 
        {
            case "buttonNext"://点击确定按钮:关闭页面，继续游戏
                {
                    GameMgr.instance.pause = false;
                    UIManager.GetInstance().HidePanel("stage_pvp");
                }break;
        }
    }


    void SetTeamPic(CampEnum _camp)
    {

    }


}

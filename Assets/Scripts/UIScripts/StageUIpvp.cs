using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����pvpս���׶�ʱ�����
/// </summary>
public class StageUIpvp : BasePanel
{
    override protected void Init()
    {
        //��ȡ��ǰA�ӵĽ�ɫ����
        SetTeamPic(CampEnum.left);
        SetTeamPic(CampEnum.right);
        
        
    }



    
    override protected void OnClick(string btnName)
    {
        switch (btnName) 
        {
            case "buttonNext"://���ȷ����ť:�ر�ҳ�棬������Ϸ
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

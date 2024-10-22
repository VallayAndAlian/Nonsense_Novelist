using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPanel : BasePanel
{
    protected override void Init()
    {
       
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "exit": //确定
                {
                    Application.Quit();

                }
                break;
            case "cancel": //我再想想
                {
                    Time.timeScale = GameMgr.instance.timeSpeed;
                    UIManager.GetInstance().HidePanel("ExitPanel");
                } break;
        }
    }

}

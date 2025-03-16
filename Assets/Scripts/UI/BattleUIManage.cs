using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManage:UIManage
{
    public List<BattleUI> BattleUiDic = new List<BattleUI>();
    public BattleUIManage()
    {}
    protected Canvas mBattleCanvas;
    public Canvas BattleCanvas => mBattleCanvas;
    
    public void ShowPanel(BattleUI ui)
    {
        if (BattleUiDic.Contains(ui))
        {
            ui.Init();
            ui.UIPanel.SetActive(true);
            return;
        }

        
        if(BattleCanvas==null)Debug.Log("BattleCanvas==null");

        BattleUiDic.Add(ui);
        ui.UIPanel.transform.SetParent(BattleCanvas.transform);
        ui.UIPanel.transform.localPosition = Vector3.zero;
        ui.UIPanel.transform.localScale = Vector3.one;
        (ui.UIPanel.transform as RectTransform).offsetMax = Vector2.zero;
        (ui.UIPanel.transform as RectTransform).offsetMin = Vector2.zero;
    }

    public void Hide(BattleUI ui)
    {
        if (!BattleUiDic.Contains(ui))
        {
            return;
        }
        ui.UIPanel.SetActive(false);
        return;
    }

    public override void Init() 
    {           
        mBattleCanvas = GameObject.Find("BattleUICanvas")?.GetComponent<Canvas>();
        if (mBattleCanvas == null)
        {
            GameObject uiCanvas = new GameObject("BattleUICanvas");
            mBattleCanvas= uiCanvas.AddComponent<Canvas>();
            mBattleCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();
            canvasRect.anchorMin = Vector2.zero;   // 锚点左下角
            canvasRect.anchorMax = Vector2.one;    // 锚点右上角
            canvasRect.offsetMin = Vector2.zero;   // 边距归零
            canvasRect.offsetMax = Vector2.zero;   // 边距归零
        }
        
    }

    public override void Update(float deltaTime)
    {
        foreach (var ui in BattleUiDic)
        {
            ui.Update(deltaTime);
        }
    }

    public override void LateUpdate(float deltaTime)
    {
        foreach (var ui in BattleUiDic)
        {
            ui.LateUpdate(deltaTime);
        }
    }

}

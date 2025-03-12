using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManage:UIManage
{
    public Dictionary<GameObject, BattleUI> BattleUiDic = new Dictionary<GameObject, BattleUI>();
    public BattleUIManage()
    {}
    protected Canvas mBattleCanvas;
    public Canvas BattleCanvas => mBattleCanvas;
    
    public void ShowPanel(GameObject panel,BattleUI ui)
    {
        if (!BattleUiDic.TryAdd(panel, ui))
        {
            ui.Init();
            panel.SetActive(true);
            return;
        }

        if(panel==null)Debug.Log("panel==null");
        
        if(BattleCanvas==null)Debug.Log("BattleCanvas==null");
        panel.transform.SetParent(BattleCanvas.transform);
        panel.transform.localPosition = Vector3.zero;
        panel.transform.localScale = Vector3.one;
        (panel.transform as RectTransform).offsetMax = Vector2.zero;
        (panel.transform as RectTransform).offsetMin = Vector2.zero;
    }

    public void Hide(BattleUI ui)
    {
        if (!BattleUiDic.TryGetValue(ui.UIPanel,out var uI))
        {
            ui.UIPanel.SetActive(false);
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
        }
        
    }

    public override void Update(float deltaTime)
    {
        foreach (var ui in BattleUiDic)
        {
            ui.Value.Update(deltaTime);
        }
    }

    public override void LateUpdate(float deltaTime)
    {
        foreach (var ui in BattleUiDic)
        {
            ui.Value.LateUpdate(deltaTime);
        }
    }

}

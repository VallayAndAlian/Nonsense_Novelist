using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIManage:UIManage
{
    public Dictionary<GameObject, BattleUI> BattleUiDic = new Dictionary<GameObject, BattleUI>();

    protected Canvas mBattleCanvas;
    public Canvas BattleCanvas => mBattleCanvas;

    public void ShowPanel(GameObject panel,BattleUI ui)
    {
        if (!BattleUiDic.TryAdd(panel, ui))
        {
            panel.SetActive(true);
            return;
        }

        panel.transform.SetParent(BattleCanvas.transform);
        panel.transform.localPosition = Vector3.zero;
        panel.transform.localScale = Vector3.one;
        (panel.transform as RectTransform).offsetMax = Vector2.zero;
        (panel.transform as RectTransform).offsetMin = Vector2.zero;
    }

    public override void Start() 
    {
        foreach (var ui in BattleUiDic)
        {
            ui.Value.Start();
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

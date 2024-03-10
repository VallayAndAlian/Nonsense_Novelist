using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂在设定Setting预制体上面
/// </summary>
public class Setting : MonoBehaviour
{
    [Header("品质概率")]
    [Header("平庸")] public int pingYong = 60;
    public GameObject py;
    [Header("巧思")] public int qiaoSi = 30;
    public GameObject qs;
    [Header("鬼才")] public int guiCai = 10;
    public GameObject gc;

    public GameObject logo;
    void Start()
    {
        Quality();
    }
    /// <summary>
    /// 随机出一个品质，并随机其中三个具体设定
    /// </summary>
    void Quality()
    {
        //概率抽取                
        int numx = UnityEngine.Random.Range(1, 101);
        if (numx <= pingYong) { numx = 0; }
        else if (numx > pingYong && numx < pingYong + qiaoSi) numx = 1;
        else if (numx >= pingYong + qiaoSi && numx < pingYong + qiaoSi + guiCai) numx = 2;

        //从品质中抽取三个设定（设定写完之后再完成）
        switch (numx)//点击确定按钮的时候push对象池
        {
            case 0: //平庸
                for(int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(py, (a) => {
                        Type py0 = AllSkills.RandomPY();
                        var ppy= a.AddComponent(py0) as AbstractSetting;
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的平庸卡牌内容
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ppy.settingName;//标题
                        a.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = ppy.info;//描述
                        //a.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("");
                                                                                                                   //生成标签
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < ppy.lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            logo.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + ppy.lables[i]);
                        }
                    });
                }
                return;
            case 1:
                for (int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(qs, (a) => {
                        Type qs0 = AllSkills.RandomQS();
                        a.AddComponent(qs0);
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的平庸卡牌内容
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = a.GetComponent<AbstractSetting>().settingName;//标题
                        a.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = a.GetComponent<AbstractSetting>().info;//描述
                                                                                                                   //a.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("");
                                                                                                                   //生成标签
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < a.GetComponent<AbstractSetting>().lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            logo.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + a.GetComponent<AbstractSetting>().lables[i]);
                        }
                    });
                }
                return;
            case 2:
                for (int j = 0; j < 3; j++)
                {
                    PoolMgr.GetInstance().GetObj(gc, (a) => {
                        Type gc0 = AllSkills.RandomQS();
                        a.AddComponent(gc0);
                        a.transform.SetParent(this.transform.GetChild(1));
                        a.transform.localScale = Vector3.one;
                        //获取抽出的平庸卡牌内容
                        a.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = a.GetComponent<AbstractSetting>().settingName;//标题
                        a.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = a.GetComponent<AbstractSetting>().info;//描述
                                                                                                                   //a.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load("");
                                                                                                                   //生成标签
                        Transform p = a.transform.GetChild(1);
                        for (int i = 0; i < a.GetComponent<AbstractSetting>().lables.Count; i++)
                        {
                            GameObject lg = Instantiate(logo);
                            lg.transform.SetParent(a.transform.GetChild(1));
                            lg.transform.localScale = Vector3.one;
                            logo.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Settinglogo/" + a.GetComponent<AbstractSetting>().lables[i]);
                        }
                    });
                }
                return;
        }
    }
    private Var button;
    public void ButtonDown()
    {
        var buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        
    }
    public void EndButton()
    {
        
    }
}

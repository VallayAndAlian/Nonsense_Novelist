using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 事件的类型
/// </summary>
[System.Serializable]
public enum EventType
{
    XiWang = 0,
    FangKe = 1,
    YiWai = 2,
    WeiJi = 3,
    JiaoYi = 4
}


public class EventUI : MonoBehaviour
{
    [Header("这是哪一个界面的UI")]
    public EventType type;



    public test1ExcelData data;
    List<test1ExcelItem> needDate;
    #region 处理data
    void DealWithData(EventType _type)
    {

        //foreach (var _t in data)
        //{
            
        //}
        switch (type)
        {
            case EventType.XiWang:
                {
                
                }
                break;
            case EventType.FangKe:
                {
                
                }
                break;
            case EventType.YiWai:
                {
                 
                }
                break;
            case EventType.JiaoYi:
                {
                    
                }
                break;
            case EventType.WeiJi:
                {
                  
                }
                break;
        }
    }
    #endregion

    #region 意外
    /// <summary>
    /// 打开意外面板时的初始刷新
    /// </summary>
    public void OpenInit_YiWai()
    {
        Transform cardParent=this.transform.Find("CardGroup");
        TextMeshProUGUI textL = this.transform.Find("L").GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI textR = this.transform.Find("R").GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI textC = this.transform.Find("C").GetComponentInChildren<TextMeshProUGUI>();

        for (int i = 0; i < 3; i++)
        {
            
        }

    }

    #endregion

    #region 访客
    public void OpenInit_FangKe()
    {

    }
    #endregion

    #region 希望
    public void OpenInit_XiWang()
    {

    }
    #endregion

    #region 交易
    public void OpenInit_JiaoYi()
    {

    }
    #endregion

    #region 危机
    public void OpenInit_WeiJi()
    {

    }
    #endregion



    #region 外部点击/调用事件


    public void Open()
    {
        switch (type)
        {
            case EventType.XiWang:
                {
                    OpenInit_XiWang();
                }
                break;
            case EventType.FangKe:
                {
                    OpenInit_FangKe();
                }
                break;
            case EventType.YiWai:
                {
                    OpenInit_YiWai();
                }
                break;
            case EventType.JiaoYi:
                {
                    OpenInit_JiaoYi();
                }
                break;
            case EventType.WeiJi:
                {
                    OpenInit_WeiJi();
                }
                break;
        }
    }


    public void ClickNext()
    {
        switch (type)
        {
            case EventType.XiWang: 
                {

                }break;
            case EventType.FangKe:
                {

                }
                break;
            case EventType.YiWai:
                {

                }
                break;
            case EventType.JiaoYi:
                {

                }
                break;
            case EventType.WeiJi:
                {

                }
                break;
        }
    }
    #endregion
}

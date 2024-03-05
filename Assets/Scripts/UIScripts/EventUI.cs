using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// �¼�������
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
    [Header("������һ�������UI")]
    public EventType type;



    public test1ExcelData data;
    List<test1ExcelItem> needDate;
    #region ����data
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

    #region ����
    /// <summary>
    /// ���������ʱ�ĳ�ʼˢ��
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

    #region �ÿ�
    public void OpenInit_FangKe()
    {

    }
    #endregion

    #region ϣ��
    public void OpenInit_XiWang()
    {

    }
    #endregion

    #region ����
    public void OpenInit_JiaoYi()
    {

    }
    #endregion

    #region Σ��
    public void OpenInit_WeiJi()
    {

    }
    #endregion



    #region �ⲿ���/�����¼�


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

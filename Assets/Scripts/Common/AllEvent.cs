using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


#region �¼��ṹ����¼�enum
/// <summary>
/// �¼�������
/// </summary>
[System.Serializable]
public enum EventType
{  
    XiWang=0,
    FangKe=1,
    YiWai=2,
     WeiJi=3,
    JiaoYi=4
  
  
}


//ÿһ�������¼�����һ���ṹ��
[System.Serializable]
public struct EventStruct
{
    public EventType type;//�¼�������

    public string name;//�¼�����
    public string textEvent;//�¼�ҳ����ʾ���ı�
    public string textDraft;//�ݸ屾�����ɵ��ı�

    //�������������������Ŵ�����
    Func<bool> triggerFunc;

    EventStruct(EventType _type, string _name, string _textEvent,string _textDraft, Func<bool> _triggerFunc)
    {
        type = _type;
        name = _name;
        textEvent = _textEvent;
        textDraft = _textDraft;
        triggerFunc = _triggerFunc;
    }
}


#endregion



///<summary>
///�����¼���̬��
///</summary>
public static class AllEvent
{
    public static List<EventStruct> AllXiWang = new List<EventStruct>();
    public static List<EventStruct> AllFangKe = new List<EventStruct>();
    public static List<EventStruct> AllYiWai= new List<EventStruct>();
    public static List<EventStruct> AllJiaoYi = new List<EventStruct>();
    public static List<EventStruct> AllWeiJi= new List<EventStruct>();

    public static List<EventStruct> AllEvents = new List<EventStruct>();
    static AllEvent()
    {
        #region ϣ���¼�

        //��Ӷ��ʴ���
      //  AllXiWang.AddRange(new EventStruct(EventType.XiWang,"","","",));
          
        #endregion
    }
 }

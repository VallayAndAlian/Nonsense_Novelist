using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


#region 事件结构体和事件enum
/// <summary>
/// 事件的类型
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


//每一个五类事件都是一个结构体
[System.Serializable]
public struct EventStruct
{
    public EventType type;//事件的类型

    public string name;//事件名称
    public string textEvent;//事件页面显示的文本
    public string textDraft;//草稿本上生成的文本

    //触发条件（满足条件才触发）
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
///所有事件静态类
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
        #region 希望事件

        //添加动词词条
      //  AllXiWang.AddRange(new EventStruct(EventType.XiWang,"","","",));
          
        #endregion
    }
 }

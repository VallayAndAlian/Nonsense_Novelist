using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 信件对象基类,用于实现鼠标点击信件等行为
/// </summary>
public class MailObjBase : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    //鼠标进入时的动作
    protected UnityAction enterAction;
    //鼠标退出时的动作
    protected UnityAction exitAction;
    //鼠标在其中点击时的动作
    protected UnityAction clickAction;

    //鼠标进入调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("鼠标进入了");
        enterAction?.Invoke();
    }

    //鼠标退出调用
    public void OnPointerExit(PointerEventData eventData)
    {
        print("鼠标退出了");
        exitAction?.Invoke();
    }

    //鼠标点击且抬起时调用
    public void OnPointerUp(PointerEventData eventData)
    {
        print("鼠标点击了");
        clickAction?.Invoke();
    }

    /* 其他可能用到事件
     IPointerExitHandler  - 	OnPointerExit  - 	当指针退出对象时调用 (鼠标离开)
     IPointerUpHandler 	 -  	OnPointerUp    - 	    松开指针时/触屏点击松开调用(抬起)
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 信件事件对象基类,用于实现鼠标点击信件等行为
/// </summary>
public class MailEventBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //鼠标进入时的动作
    protected UnityAction enterAction;
    //鼠标退出时的动作
    protected UnityAction exitAction;
    //鼠标在其中点击时的动作
    protected UnityAction clickAction;


    //鼠标进入调用
    public void OnPointerEnter(PointerEventData eventData) => enterAction?.Invoke();
    //鼠标退出调用
    public void OnPointerExit(PointerEventData eventData) => exitAction?.Invoke();
    //鼠标点击调用
    public void OnPointerClick(PointerEventData eventData) => clickAction?.Invoke();

}

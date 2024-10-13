using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// �ż��¼��������,����ʵ��������ż�����Ϊ
/// </summary>
public class MailEventBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //������ʱ�Ķ���
    protected UnityAction enterAction;
    //����˳�ʱ�Ķ���
    protected UnityAction exitAction;
    //��������е��ʱ�Ķ���
    protected UnityAction clickAction;


    //���������
    public void OnPointerEnter(PointerEventData eventData) => enterAction?.Invoke();
    //����˳�����
    public void OnPointerExit(PointerEventData eventData) => exitAction?.Invoke();
    //���������
    public void OnPointerClick(PointerEventData eventData) => clickAction?.Invoke();

}

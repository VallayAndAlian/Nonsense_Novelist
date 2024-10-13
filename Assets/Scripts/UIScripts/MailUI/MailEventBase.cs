using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// �ż��¼��������,����ʵ��������ż�����Ϊ
/// </summary>
public class MailEventBase : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
{
    //������ʱ�Ķ���
    protected UnityAction enterAction;
    //����˳�ʱ�Ķ���
    protected UnityAction exitAction;
    //��������е��ʱ�Ķ���
    protected UnityAction clickAction;

    //���������
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("��������");
        enterAction?.Invoke();
    }

    //����˳�����
    public void OnPointerExit(PointerEventData eventData)
    {
        print("����˳���");
        exitAction?.Invoke();
    }

    //�������̧��ʱ����
    public void OnPointerUp(PointerEventData eventData)
    {
        print("�������");
        clickAction?.Invoke();
    }

    /* ���������õ��¼�
     IPointerExitHandler  - 	OnPointerExit  - 	��ָ���˳�����ʱ���� (����뿪)
     IPointerUpHandler 	 -  	OnPointerUp    - 	    �ɿ�ָ��ʱ/��������ɿ�����(̧��)
    */
}

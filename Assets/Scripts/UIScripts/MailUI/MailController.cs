using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// �ż�������,���ڼ���ż��¼���������Ϊ,�����Ƴ��͵��
/// </summary>
public class MailController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /// <summary>
    /// ���Ƶ��ż�����
    /// </summary>
    public MailObj mailObj;

    //������ʱ�Ķ���
    protected UnityAction enterAction;
    //����˳�ʱ�Ķ���
    protected UnityAction exitAction;
    //��������е��ʱ�Ķ���
    protected UnityAction clickAction;

    //��ʼ������Ƶ��ż�����
    //private void Awake()
    //{
    //    mailObj = transform.GetComponentInParent<MailObj>();
    //}

    //ע���ż�����¼�
    private void Start()
    {
        if (mailObj == null)
            return;

        /* ע������¼� */
        //������:UI�ϸ���
        enterAction += () =>
        {
            mailObj.MoveTarget(mailObj.floatPos, moveDir.ToTop);
        };

        //����˳�:UI�¸���
        exitAction += () =>
        {
            mailObj.MoveTarget(mailObj.originPos, moveDir.ToDown);
        };

        //�����
        clickAction += () =>
        {
            /* ���ż�����:����MailInfo,��ʾ�ż�����,ȡ��δ���ķ�����ʾ��
            */
            mailObj.ClickAction();
        };
    }

    //���������
    public void OnPointerEnter(PointerEventData eventData) => enterAction?.Invoke();
    //����˳�����
    public void OnPointerExit(PointerEventData eventData) => exitAction?.Invoke();
    //���������
    public void OnPointerClick(PointerEventData eventData) => clickAction?.Invoke();
}

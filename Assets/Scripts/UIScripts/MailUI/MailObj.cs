using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum moveDir
{
    ToTop,
    ToDown
}

public class MailObj : MailObjBase
{
    //����ͼƬ
    public Image bgImg;
    //[����ʹ��]:�ż���ǩ,ɸѡ����
    public TextMeshProUGUI textTag;

    //�ż�UIԭλ��
    private Vector3 originPos;
    //�ż�UI����Ŀ��λ��
    private Vector3 floatPos;
    //�ż�����
    private MailInfo mailInfo;

    private void Start()
    {
        //��ʼ����¼ԭ���ż�λ�ú͸�����Ŀ��λ��
        originPos = this.transform.localPosition;
        floatPos = originPos + new Vector3(0, 30, 0);

        //��ʼ���ż���ϢΪNULL
        if (mailInfo == null)
            SetMailInfo(new MailInfo(E_MailAuther.NULLAuther));
        //��ʾ��ʼ��ǩ����
        textTag.text = mailInfo.auther.ToString();

        /* ע������¼� */
        //������:UI�ϸ���
        enterAction += () =>{
            MoveTarget(floatPos,moveDir.ToTop);
         };
        //����˳�:UI�¸���
        exitAction += () =>
        {
            MoveTarget(originPos, moveDir.ToDown);
        };
        //�����
        clickAction += () =>
        {
            /* ���ż�����UI����:����MailInfo,��ʾ�ż�����
               ȡ��δ���ķ�����ʾ��
            */
            ClickAction();
        };
    }

    /// <summary>
    /// ������������ż���,�ż������ͻظ�
    /// </summary>
    /// <param name="targetPos">Ŀ��λ��</param>
    /// <param name="dir">��������</param>
    private void MoveTarget(Vector3 targetPos,moveDir dir)
    {
        //��ǰλ��
        Vector3 curPos = this.transform.localPosition;
        //ÿ�غ�ƫ����
        Vector3 delta = dir == moveDir.ToTop ? 
            new Vector3(0, Time.deltaTime, 0) : -new Vector3(0, Time.deltaTime, 0);
        //����ƫ����
        while (true)
        {
            curPos += delta;
            if (dir == moveDir.ToTop && curPos.y > targetPos.y)
            {
                curPos = targetPos;
                break;
            }
            else if(dir == moveDir.ToDown && curPos.y < targetPos.y)
            {
                curPos = targetPos;
                break;
            }
            this.transform.localPosition = curPos;
        }
    }

    /// <summary>
    /// �����ż�����MailInfo,��ˢ��ҳ����ʾ
    /// </summary>
    /// <param name="mailInfo"></param>
    public void SetMailInfo(MailInfo mailInfo)
    {
        this.mailInfo = mailInfo;

        //��������������ҳ����ʾ
        textTag.text = mailInfo.auther.ToString();
        //������Ϣ��UI�������ü�����ʾ...
    }
    
    /// <summary>
    /// ������ż���Ķ���
    /// </summary>
    public void ClickAction()
    {
        //���ż�
        /*
         ���ż�����UI����:����MailInfo,��ʾ�ż�����
         ȡ��δ���ķ�����ʾ��,���洢�ż����״̬
         */
    }

}

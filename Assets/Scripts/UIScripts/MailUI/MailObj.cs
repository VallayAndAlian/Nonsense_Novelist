using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum moveDir
{
    ToTop,
    ToDown
}

public class MailObj : MailEventBase
{
    //����ͼƬ
    public Image bgImg;
    //dear�ż�ͷ��
    public TextMeshProUGUI dearText;
    //�ż�����
    public TextMeshProUGUI contentText;
    //�ż�����
    public TextMeshProUGUI authorText;

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
            ShowPreMail(new MailInfo(E_MailAuther.δ֪������));

        /* ע������¼� */
        //������:UI�ϸ���
        enterAction += () =>
        {
            MoveTarget(floatPos, moveDir.ToTop);
        };
        //����˳�:UI�¸���
        exitAction += () =>
        {
            MoveTarget(originPos, moveDir.ToDown);
        };
        //�����
        clickAction += () =>
        {
            print("�������");
            /* ���ż�����UI����:����MailInfo,��ʾ�ż�����
               ȡ��δ���ķ�����ʾ��
            */
            ClickAction();
        };
    }

    /// <summary>
    /// �ż����������Ч��
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
    /// ����MailInfo����ˢ��ҳ����Ϣ,����ʾ�ż�
    /// </summary>
    /// <param name="mailInfo"></param>
    public void ShowPreMail(MailInfo mailInfo)
    {
        //��������������ҳ����ʾ
        this.mailInfo = mailInfo;
        this.dearText.text = mailInfo.dear;
        this.contentText.text = mailInfo.mailBody;
        this.authorText.text = mailInfo.auther.ToString();
        //MailInfo������Ϣ������ʾ

        //��ʾ����
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// ���ش�Ԥ���ż�
    /// </summary>
    public void HidePreMail()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// ������ż���Ķ���
    /// </summary>
    public void ClickAction()
    {
        //�����ż�����mailInfo,������巽��,���ż�����
        MailDetailPanel.Instance.ShowMailDetailInfo(this.mailInfo);
        //����Ԥ�����
        MailPreviewPanel.Instance.Hide();
    }

}

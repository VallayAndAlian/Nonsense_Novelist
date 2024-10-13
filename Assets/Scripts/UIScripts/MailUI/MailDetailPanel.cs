using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MailDetailPanel : BasePanel<MailDetailPanel>
{
    //���ذ�ť
    public Button backBtn;
    //�ż�����
    public TextMeshProUGUI contentText;
    //������
    public TextMeshProUGUI dearText;
    //��������
    public TextMeshProUGUI scoreText;
    //����˰�ť��ȡ����
    public Button attchBtn;
    //�ż���Ϣ
    private MailInfo mailInfo;

    //���渽���б�:��UpdateDetailInfo��������
    protected override void Init()
    {
        //���ذ�ť
        backBtn.onClick.AddListener(() => {
            //��ǰ�������
            Hide();
            //��ʾԤ�����
            MailPreviewPanel.Instance.Show();
        });

        //��ȡ����:����MailInfo�е�`����id������`��ȡ
        attchBtn.onClick.AddListener(() => {

        });

        //��ʼ����ɺ�,���������ʱ����
        Hide();
    }

    /// <summary>
    /// ���������ʾ��Ϣ:���ݴ����MailInfo
    /// </summary>
    public void ShowMailDetailInfo(MailInfo info)
    {
        this.mailInfo = info;
        Show();
    }

    /// <summary>
    /// ��д�����Show,��ÿ����ʾʱ�����ż���Ϣ��ʾ
    /// </summary>
    public override void Show()
    {
        dearText.text = mailInfo.dear;
        contentText.text = mailInfo.mailBody;
        scoreText.text = mailInfo.score.ToString();
        base.Show();
    }
}

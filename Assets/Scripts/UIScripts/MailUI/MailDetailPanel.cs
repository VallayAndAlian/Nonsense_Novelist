using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailDetailPanel : MonoBehaviour
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

    //���渽���б�:��UpdateDetailInfo��������
    void Start()
    {
        //����:�������
        backBtn.onClick.AddListener(() => {
            Hide();
        });

        //��ȡ����
        attchBtn.onClick.AddListener(() => { 
            
        });
    }

    /// <summary>
    /// ���������ʾ��Ϣ:���ݴ����MailInfo
    /// </summary>
    public void UpdateDetailInfo(MailInfo info)
    {
        dearText.text = info.dear;
        contentText.text = info.mailBody;
        scoreText.text = info.score.ToString();
    }

    public void Hide() 
    {
        this.gameObject.SetActive(false);
        //��ո����б�
    }

}

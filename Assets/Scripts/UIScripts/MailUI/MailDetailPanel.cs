using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MailTable;

public class MailDetailPanel : BasePanel<MailDetailPanel>
{
    //���ذ�ť
    public Button backBtn;
    //�ż����ݱ���ͼƬ
    public Image contentImage;
    //������������
    public TextMeshProUGUI scoreText;
    //��������ͼƬ����
    public Image scoreTitleImg;
    //�������ֵ�ͼ
    public Image scorebgImg;
    //�ռ���
    public TextMeshProUGUI dearText;
    //�ż�����
    public TextMeshProUGUI contentText;
    //�½�β��
    public TextMeshProUGUI autherText;
    //����˰�ť��ȡ����
    public Button attchBtn;
    
    //�ż���Ϣ
    private MailInfo mailInfo;
    //����ͼƬ·��
    private string imgPath = "UI/Mail/";

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
            //��ȡ����
            tackAttach();
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
        autherText.text = mailInfo.autherName;
        scoreText.text = mailInfo.score.ToString();
        
        //�����Ѿ���ȡ,��ȡ����ȡ��ť,�����ظ���ȡ
        if(mailInfo.attachIsTake)
            attchBtn.gameObject.SetActive(false);

        //ֻ�б���༭��ʾ��������ģ��
        if(mailInfo.autherType == E_MailAutherType.BaoShe)
        {
            scoreText.gameObject.SetActive(true);
            scoreTitleImg.gameObject.SetActive(true);
            scorebgImg.gameObject.SetActive(true);
        }
        else
        {
            scoreText.gameObject.SetActive(false);
            scoreTitleImg.gameObject.SetActive(false);
            scorebgImg.gameObject.SetActive(false);
        }
        //�����ż�������ʾ����ͼƬ
        switch (mailInfo.autherType)
        {
            //����
            case E_MailAutherType.BaoShe:
                contentImage.sprite = LoadImg(imgPath + "officepaper");
                break;
            //����³
            case E_MailAutherType.KeLao:
                contentImage.sprite = LoadImg(imgPath + "doctorpaper");
                break;
            //��˿�˵�
            case E_MailAutherType.BiDe:
                contentImage.sprite = LoadImg(imgPath + "fanspaper");
                break;
            //����ľ
            case E_MailAutherType.WenTeCen:
                contentImage.sprite = LoadImg(imgPath + "friendpaper");
                break;
        }
        base.Show();
    }

    /// <summary>
    /// ����ͼƬ
    /// </summary>
    /// <param name="imgPath"></param>
    /// <returns></returns>
    public Sprite LoadImg(string imgPath)
    {
        Sprite sprite = Resources.Load<Sprite>(imgPath);
        //ʹ�ü��ص�ͼƬ����Ĭ��ͼƬ
        return sprite ?? contentImage.sprite;
    }

    public void tackAttach()
    {

        //�������ݳ־û������洢�Ѿ���ȡ��״̬
        
        //��ȡ���ݸ���id��ȡ�ż����߼�
        this.mailInfo.attachIsTake = false;
    }
}

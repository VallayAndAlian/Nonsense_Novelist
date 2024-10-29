using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailDetailPanel : BasePanel<MailDetailPanel>
{
    //���ذ�ť
    public Button backBtn;
    //�ż����ݱ���ͼƬ
    public Image contentImage;
    //��������
    public TextMeshProUGUI scoreText;
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
        //�����ż�������ʾ����ͼƬ
        switch (mailInfo.autherType)
        {
            //����
            case E_MailAutherType.BaoShe:
                contentImage.sprite = LoadImg(imgPath + "officepaper");
                break;
            //����³
            case E_MailAutherType.AnDelu:
                contentImage.sprite = LoadImg(imgPath + "doctorpaper");
                break;
            //��˿�˵�
            case E_MailAutherType.BiDe:
                contentImage.sprite = LoadImg(imgPath + "fanspaper");
                break;
            //����ľ
            case E_MailAutherType.ZuoZuoMu:
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
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MailTable;

public enum moveDir
{
    ToTop,
    ToDown
}

/// <summary>
/// Ԥ���ż�����
/// </summary>
public class PreMailObj : MonoBehaviour
{
    //����ͼƬ
    public Image bgImg;
    //�ռ���
    public TextMeshProUGUI dearText;
    //�ż�����
    public TextMeshProUGUI contentText;
    //������
    public TextMeshProUGUI authorText;
    //�жϿ�
    public GameObject ctrlerImgObj;

    //�ż�UIԭλ��
    [HideInInspector]
    public Vector3 originPos;
    //�ż�UI����Ŀ��λ��
    [HideInInspector]
    public Vector3 floatPos;
    
    //�ż�����
    private MailInfo mailInfo;
    //����ͼƬ·��
    private string imgPath = "UI/Mail/";
    
    private void Start()
    {
        //��ʼ����¼ԭ���ż�λ�ú͸�����Ŀ��λ��
        originPos = this.transform.localPosition;
        floatPos = originPos + new Vector3(0, 300, 0);

        //��ʼ���ż���ϢΪNULL
        if (mailInfo == null)
            Show(new MailInfo(E_MailAutherType.Default));
    }

    /// <summary>
    /// �ż����������Ч��
    /// </summary>
    /// <param name="targetPos">Ŀ��λ��</param>
    /// <param name="dir">��������</param>
    public void MoveTarget(Vector3 targetPos,moveDir dir)
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
    public void Show(MailInfo mailInfo)
    {
        //��������������ҳ����ʾ
        this.mailInfo = mailInfo;
        this.dearText.text = mailInfo.dear;
        this.contentText.text = mailInfo.mailBody;
        this.authorText.text = mailInfo.autherName;

        //�����ż�������ʾ����ͼƬ
        switch (mailInfo.autherType)
        {
            //����
            case E_MailAutherType.BaoShe:
                bgImg.sprite = LoadImg(imgPath + "officepaper");
                break;
                //����³
            case E_MailAutherType.KeLao:
                bgImg.sprite = LoadImg(imgPath + "doctorpaper");
                break;
                //��˿�˵�
            case E_MailAutherType.BiDe:
                bgImg.sprite = LoadImg(imgPath + "fanspaper");
                break;
                //����ľ
            case E_MailAutherType.WenTeCen:
                bgImg.sprite = LoadImg(imgPath + "friendpaper");
                break;
        }

        //MailInfo������Ϣ������ʾ

        //��ʾ����
        this.gameObject.SetActive(true);
        ctrlerImgObj.SetActive(true);
    }

    public Sprite LoadImg(string imgPath)
    {
        Sprite sprite = Resources.Load<Sprite>(imgPath);
        //ʹ�ü��ص�ͼƬ����Ĭ��ͼƬ
        return sprite ?? bgImg.sprite;
    }

    /// <summary>
    /// ���ش�Ԥ���ż������жϿ�
    /// </summary>
    public void Hide()
    {
        this.gameObject.SetActive(false);
        ctrlerImgObj.SetActive(false);
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

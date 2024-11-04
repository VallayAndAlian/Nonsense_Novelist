/// <summary>
/// �ż�����,������ɸѡ
/// </summary>
public enum E_MailAutherType
{
    Default,
    BaoShe,
    AnDelu,
    BiDe,
    ZuoZuoMu,
}

/// <summary>
/// �ż���Ϣ��
/// ���ø�ʽ{�ż�id,�ż�����,�ż��ƺ�,�ż�����}
/// </summary>
public class MailInfo
{
    //�ż����
    public int id;
    //�ż�����
    public string mailName;
    //����������:���ַ�����,ͬһ���������Ϳ����Բ�ͬ�ƺ�����
    public E_MailAutherType autherType;
    //������ʵ����ʾ����
    public string autherName;
    //�ƺ�����:�����˶��ռ��˵ĳƺ�
    public string dear;
    //�ż�����
    public string mailBody;
    //�ż���������
    public int score;
    //�Ƿ��Ѷ�
    public bool isRead;
    //�Ƿ���ʾ:��ʱ�ż��Ƿ���������
    public bool isDisplay;
    //����id
    public int attachId;
    //��������
    public int attachNum;
    //�����Ƿ��Ѿ�����ȡ(�ó�)
    public bool attachIsTake;

    public MailInfo()
    {
        
    }

    /// <summary>
    /// ����ʼ��auther�ֶε��ż�
    /// </summary>
    /// <param name="auther"></param>
    public MailInfo(E_MailAutherType auther)
    {
        this.autherType = auther;
    }

}


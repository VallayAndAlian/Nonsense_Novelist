/// <summary>
/// �ż�����,������ɸѡ
/// </summary>
public enum E_MailAuther
{
    ����༭,
    ����³ҽ��,
    ������ʵ��˿�˵�,
    ����ľ�༭,
    δ֪������,
}

/// <summary>
/// �ż���Ϣ��
/// ���ø�ʽ{�ż�id,�ż�����,�ż��ƺ�,�ż�����}
/// </summary>
public class MailInfo
{
    //�ż����
    public int id;
    //����������:���ַ�����,ͬһ���������Ϳ����Բ�ͬ�ƺ�����
    public E_MailAuther autherType;
    //������ʵ����ʾ����
    public string autherName;
    //�ƺ�����:Ҳ����Ը��ݷ������Ż�
    public string dear;
    //�ż�����
    public string mailBody;
    //�ż���������
    public int score;
    //�Ƿ��Ѷ�
    public bool isRead;
    //�Ƿ�Ӧ����ʾ
    public bool isDisPlay;
    //����id
    public int attchId;
    //��������
    public int attchNum;
    //�����Ƿ��Ѿ�����ȡ(�ó�)
    public string attchIsOut;

    public MailInfo()
    {
        
    }

    /// <summary>
    /// ����ʼ��auther�ֶε��ż�
    /// </summary>
    /// <param name="auther"></param>
    public MailInfo(E_MailAuther auther)
    {
        this.autherType = auther;
    }

}


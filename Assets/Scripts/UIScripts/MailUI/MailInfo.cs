/// <summary>
/// �ż�����,������ɸѡ
/// </summary>
public enum E_MailAuther
{
    Auther1,
    Auther2,
    Auther3,
    NULLAuther
}

/// <summary>
/// �ż���Ϣ��
/// ���ø�ʽ{�ż�id,�ż�����,�ż��ƺ�,�ż�����}
/// </summary>
public class MailInfo
{
    //�ż����
    public int id;
    //������
    public E_MailAuther auther;
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
    //��������:����Ϊid��,���������������Ϸ�еĵ���
    public string attch;
    //��������
    public string attchNum;
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
        this.auther = auther;
    }

}


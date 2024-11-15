using System;

/// <summary>
/// �ż�����,������ɸѡ
/// </summary>
public enum E_MailAutherType
{
    /// <summary>
    /// Ĭ����Ϣ,�յ��ʼ���������
    /// </summary>
    Default,
    /// <summary>
    /// ����༭
    /// </summary>
    BaoShe,
    /// <summary>
    /// ����³ҽ��
    /// </summary>
    KeLao,
    /// <summary>
    /// ��ʵ��˿�˵�
    /// </summary>
    BiDe,
    /// <summary>
    /// ����ɭ
    /// </summary>
    WenTeCen,
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
    /// ����ʼ��auther�ֶε��ż�[���Խ׶�ʹ��]
    /// </summary>
    /// <param name="auther"></param>
    [Obsolete("MailInfo�н���ʼ���ż����͵ķ���,�÷��������ڲ���ʹ��")]
    public MailInfo(E_MailAutherType auther)
    {
        this.autherType = auther;
    }

}


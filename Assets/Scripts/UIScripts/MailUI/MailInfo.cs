using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ż�����,������ɸѡ
/// </summary>
public enum E_MailAuther 
{
    
}

/// <summary>
/// �ż���Ϣ��
/// ���ø�ʽ{�ż�id,�ż�����,�ż��ƺ�,�ż�����}
/// </summary>
public class MailInfo
{
    //�½����
    public int id;
    //������
    public E_MailAuther auther;
    //�ƺ�����
    public string dearContent;
    //�ż�������
    public string mailBody;
    //�ż���������
    public int score;

    //��������:��ǰ�洢·��,���ܻ����Ԥ������Ϊ������
    public string path;
}


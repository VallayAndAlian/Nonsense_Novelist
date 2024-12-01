
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailTable : JsonTable<MailTable.Data>
{
    public override string AssetName => "MailTable";

    public class Data
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
    }

    public override TableErrorMeta Parse(string text)
    {
        return base.Parse(text);
    }
}

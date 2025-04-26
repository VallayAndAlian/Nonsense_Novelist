using OfficeOpenXml.ConditionalFormatting.Contracts;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using static UnityEditor.Progress;

public class MailDataManager : Save
{
    //��������
    public static MailDataManager instance = new MailDataManager();
    public static MailDataManager Instance => instance;
    private MailDataManager() { }

    //�ż�����<��̬id,�ż�����>
    private Dictionary<int, MailInfo> dataList = new Dictionary<int, MailInfo>();
    public Dictionary<int,MailInfo> DataList => dataList;
    public override string mFileName => "mail";
    
    //�ż���̬����ID:��ʼid�͵�ǰ���㵽�����id
    private int startDID = 1;
    public int currentDID = 0;

    /// <summary>
    /// ���һ��������id���ż�����,���������
    /// </summary>
    /// <param name="id"></param>
    public void CreateMail(int id)
    {
        //����Ƿ���Ը��ô�ǰid
        bool flag = false;
        int tempId = startDID;
        for(int i = startDID; i < currentDID; i++)
        {
            if(!dataList.ContainsKey(i))
            {
                flag = true;
                tempId = i;
                break;
            }
        }
        if(!flag)
        {
            currentDID++;
            tempId = currentDID;
        }
        dataList.Add(tempId,new MailInfo(id));
    }

    /// <summary>
    /// ͨ��id�Ƴ��ż�
    /// </summary>
    /// <param name="id">�ż�id</param>
    public void Remove(int id) 
    { 
        //�Ƴ��ż�
        if(dataList.ContainsKey(id))
            dataList.Remove(id);
    }

    /// <summary>
    /// �����ż�
    /// </summary>
    /// <param name="info"></param>
    public void Cover(MailInfo info)
    {
        if(dataList.ContainsKey(info.id))
            dataList[info.id]= info;
    }

    public override bool WriteA(SaveHandler writer)
    {
        //��д����
        writer.Write<int>(dataList.Count);
        
        //������л�
        foreach (var item in DataList.Values)
        {
            //key
            writer.Write(item.dId);
            //���key��Ӧ��value
            writer.Write(item.id);
            writer.Write(item.isRead);
            writer.Write(item.isDisplay);
            writer.Write(item.score);
            writer.Write(item.attachIsTake);
        }

        return true;
    }

    public override bool ReadA(SaveHandler reader)
    {
        //���
        dataList.Clear();

        //�ȶ�����
        int length = reader.Read<int>();
        int key = 0;
        int id = 0;
        MailInfo mailinfo;
        for (int i = 0; i < length; i++)
        {
            key = reader.Read<int>();
            id = reader.Read<int>();
            mailinfo = new MailInfo(id);
            mailinfo.dId = key;
            mailinfo.isRead  = reader.Read<bool>();
            mailinfo.isDisplay = reader.Read<bool>();
            mailinfo.score = reader.Read<int>();
            mailinfo.attachIsTake = reader.Read<bool>();
            dataList.Add(key, mailinfo);
        }
        return true;
    }
}

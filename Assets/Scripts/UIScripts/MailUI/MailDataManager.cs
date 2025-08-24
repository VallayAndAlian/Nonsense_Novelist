using System.Collections.Generic;

public class MailDataManager : Save
{
    //��������
    private static MailDataManager instance;
    public static MailDataManager Instance 
    { 
        get{ 
            if (instance == null) 
                instance = new MailDataManager(); 
            return instance; 
        }
    }
    private MailDataManager() { }

    //�ż�����<��̬id,�ż�����>
    private Dictionary<int, MailInfo> dataList = new Dictionary<int, MailInfo>();
    public Dictionary<int,MailInfo> DataList => dataList;
    //�־û��ļ�����
    public override string mFileName => "mail";
    
    //�ż���̬����ID:��ʼid�͵�ǰ���㵽�����id
    private int startDID = 1;
    private int currentDID = 0;

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
        //ÿ�������б仯����³־û��洢
        SaveData();
    }

    /// <summary>
    /// ���һ��������id���ż�����,���������,�������ż��ĸ�������
    /// </summary>
    /// <param name="id">�ż���id</param>
    /// <param name="attachId">�����ĵ���id</param>
    /// <param name="attachNum">����������</param>
    public void CreateMail(int id, int attachId, int attachNum)
    {
        //����Ƿ���Ը��ô�ǰid
        bool flag = false;
        int tempId = startDID;
        for (int i = startDID; i < currentDID; i++)
        {
            if (!dataList.ContainsKey(i))
            {
                flag = true;
                tempId = i;
                break;
            }
        }
        if (!flag)
        {
            currentDID++;
            tempId = currentDID;
        }
        dataList.Add(tempId, new MailInfo(id, attachId, attachNum));
        //ÿ�������б仯����³־û��洢
        SaveData();
    }

    /// <summary>
    /// �����ż�������ID���ö�������
    /// </summary>
    /// <param name="id"></param>
    /// <param name="score"></param>
    public void CreateMail(int id, int score)
    {
        //ֻ�б���༭���Ͳ��ж�������
        //����Ƿ���Ը��ô�ǰid
        bool flag = false;
        int tempId = startDID;
        for (int i = startDID; i < currentDID; i++)
        {
            if (!dataList.ContainsKey(i))
            {
                flag = true;
                tempId = i;
                break;
            }
        }
        if (!flag)
        {
            currentDID++;
            tempId = currentDID;
        }
        dataList.Add(tempId, new MailInfo(id,score));
        //ÿ�������б仯����³־û��洢
        SaveData();
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

    /// <summary>
    /// serialize
    /// </summary>
    /// <param name="writer"></param>
    /// <returns></returns>
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

    /// <summary>
    /// deserialize
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
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

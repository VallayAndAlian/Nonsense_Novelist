using System.Collections.Generic;

public class MailDataManager : Save
{
    //单例对象
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

    //信件数据<动态id,信件数据>
    private Dictionary<int, MailInfo> dataList = new Dictionary<int, MailInfo>();
    public Dictionary<int,MailInfo> DataList => dataList;
    //持久化文件名称
    public override string mFileName => "mail";
    
    //信件动态数据ID:初始id和当前计算到的最大id
    private int startDID = 1;
    private int currentDID = 0;

    /// <summary>
    /// 添加一个基于主id的信件对象,放入管理器
    /// </summary>
    /// <param name="id"></param>
    public void CreateMail(int id)
    {
        //标记是否可以复用此前id
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
        //每当数据有变化便更新持久化存储
        SaveData();
    }

    /// <summary>
    /// 添加一个基于主id的信件对象,放入管理器,并设置信件的附件内容
    /// </summary>
    /// <param name="id">信件的id</param>
    /// <param name="attachId">附件的道具id</param>
    /// <param name="attachNum">附件的数量</param>
    public void CreateMail(int id, int attachId, int attachNum)
    {
        //标记是否可以复用此前id
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
        //每当数据有变化便更新持久化存储
        SaveData();
    }

    /// <summary>
    /// 加入信件并根据ID设置读者评分
    /// </summary>
    /// <param name="id"></param>
    /// <param name="score"></param>
    public void CreateMail(int id, int score)
    {
        //只有报社编辑类型才有读者评分
        //标记是否可以复用此前id
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
        //每当数据有变化便更新持久化存储
        SaveData();
    }

    /// <summary>
    /// 通过id移除信件
    /// </summary>
    /// <param name="id">信件id</param>
    public void Remove(int id) 
    { 
        //移除信件
        if(dataList.ContainsKey(id))
            dataList.Remove(id);
    }

    /// <summary>
    /// 覆盖信件
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
        //先写长度
        writer.Write<int>(dataList.Count);
        
        //逐个序列化
        foreach (var item in DataList.Values)
        {
            //key
            writer.Write(item.dId);
            //这个key对应的value
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
        //清空
        dataList.Clear();

        //先读长度
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

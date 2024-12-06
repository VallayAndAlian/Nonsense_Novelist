using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class MailDataManager : Save
{
    //单例对象
    public static MailDataManager instance = new MailDataManager();
    public static MailDataManager Instance => instance;
    private MailDataManager() { }

    //信件数据<动态id,信件数据>
    private Dictionary<int, MailInfo> dataList = new Dictionary<int, MailInfo>();
    public Dictionary<int,MailInfo> DataList => dataList;
    public override string mFileName => "mail";
    
    //信件动态数据ID:初始id和当前计算到的最大id
    private int startDID = 1;
    public int currentDID = 0;

    public override bool Read(BinaryFormatter binary, string path)
    {

        return false;
    }

    public override bool Write(BinaryFormatter binary, string path)
    {

        return false;
    }

    /// <summary>
    /// 添加一个基于主id的信件对象
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
    
}

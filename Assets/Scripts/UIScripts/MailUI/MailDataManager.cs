using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

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

    public override bool Read(BinaryFormatter binary, string path)
    {

        return false;
    }

    public override bool Write(BinaryFormatter binary, string path)
    {

        return false;
    }

    /// <summary>
    /// ���һ��������id���ż�����
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
    
}

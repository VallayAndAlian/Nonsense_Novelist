using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// ���ɵ��ı�����
/// </summary>
public enum E_BattleTextType 
{
    /// <summary>
    /// �����ɶ�:ÿ�����ɵ��ı��Գ�һ����Ȼ�Σ���Ҫ����
    /// </summary>
    Simple = 1,
    /// <summary>
    /// �������:���������ı�,���������ı�ÿ3�κϲ�Ϊһ����Ȼ�Σ��ϲ���ʽΪֱ������
    /// </summary>
    Gruop,
    /// <summary>
    /// �������ı�:�����ı���Ϊһ��,Frame������ָ���Ŀ��ƴ�ӵ��ݸ屾��
    /// </summary>
    Connection,
    /// <summary>
    /// ������
    /// </summary>
    Frame,
}

public class BattleTexManager : SingletonBase<BattleTexManager>
{
    /// <summary>
    /// �����<�ı�����,����>
    /// </summary>
    private Dictionary<E_BattleTextType, List<BattleTextTable.Data>> bufferPool = new();
    /// <summary>
    /// �����Ϳ�����Ϣ������
    /// </summary>
    private Dictionary<E_BattleTextType, int> numDic = new()
    {
        { E_BattleTextType.Simple, 0 },
        { E_BattleTextType.Gruop, 0 },
        { E_BattleTextType.Connection, 0 },
        { E_BattleTextType.Frame, 0 },
    };

    /// <summary>
    /// �ı�����
    /// </summary>
    /// <param name="id">�����ı���Id</param>
    /// <param name="generateType">��������</param>
    public void GenerateText(int id, E_BattleTextType generateType)
    {
        BattleTextTable.Data textInfo = BattleTextTable.Find(id);
        if (textInfo == null)
        {
            Debug.Log($"[�ı�������]���ı����ɱ��Ҳ�������Id:{id}");
            return;
        }
        switch (generateType)
        {
            case E_BattleTextType.Simple:
                //���ı��Զ�����Ȼ�ν���
                
                break;
            case E_BattleTextType.Gruop:
                //�㹻3��ʱ,ֱ��ƴ�ӵ�ԭ�����ı���
                AddTextData(id);
                if (GetTextCount(generateType) >= 3) 
                {
                    List<BattleTextTable.Data> textDatas = GetTextData(generateType, 3);
                    StringBuilder str = new StringBuilder();
                    str.Append(textDatas[0].content);
                    str.Append(textDatas[1].content);
                    str.Append(textDatas[2].content);
                    //�����ı�
                    str.ToString();
                }
                break;
            case E_BattleTextType.Connection:
                AddTextData(id);
                //�㹻����ʱ,���п���Frame����,������ƴ���Զ�����Ȼ��д������
                if (GetTextCount(E_BattleTextType.Connection) >= 3 && GetTextCount(E_BattleTextType.Frame) >= 1)   
                {
                    List<BattleTextTable.Data> textDatas = GetTextData(generateType, 3);
                    string part1 = textDatas[0].content;
                    string part2 = textDatas[1].content;
                    string part3 = textDatas[2].content;
                    string frame = GetTextData(E_BattleTextType.Frame)[0].content;
                    //������Ȼ�ν���
                    string battleText = string.Format(frame, part1, part2, part3);
                }
                break;
            case E_BattleTextType.Frame:
                AddTextData(id);
                //��Connection�㹻3��,ʹ��FrameΪ���,ƴ��Connection�����Զ�����Ȼ��д������
                if (GetTextCount(E_BattleTextType.Connection) >= 3 && GetTextCount(E_BattleTextType.Frame) >= 1)
                {
                    List<BattleTextTable.Data> textDatas = GetTextData(generateType, 3);
                    string part1 = textDatas[0].content;
                    string part2 = textDatas[1].content;
                    string part3 = textDatas[2].content;
                    string frame = GetTextData(E_BattleTextType.Frame)[0].content;
                    //������Ȼ�ν���
                    string battleText = string.Format(frame, part1, part2, part3);
                }
                break;
        }
    }    
    
    /// <summary>
    /// ������ݵ�����
    /// </summary>
    /// <param name="battleTextId">�ı���Id</param>
    private void AddTextData(int battleTextId)
    {
        BattleTextTable.Data battleInfo = BattleTextTable.Find(battleTextId);
        if (battleInfo == null)
        {
            Debug.LogWarning($"[BattleTextPoolBuffer] ���Լ��벻���ڵ�ս���ı�����{battleTextId}");
            return;
        }
        //��Ϣ�洢
        if (!bufferPool.ContainsKey(battleInfo.type))
            bufferPool.Add(battleInfo.type, new List<BattleTextTable.Data>());
        bufferPool[battleInfo.type].Add(battleInfo);
        //�������μ�¼
        numDic[battleInfo.type]++;
    }

    /// <summary>
    /// ��ָ���������ȡ��ָ����������
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private List<BattleTextTable.Data> GetTextData(E_BattleTextType type, int count = 1)
    {
        if (!bufferPool.ContainsKey(type) || GetTextCount(type) < count)
        {
            Debug.LogWarning($"[BattleTextPoolBuffer] ���Ի�ȡ�����ڵ���Ϣ����Ϣ�������㹻{type}");
            return null;
        }
        //��Ž��
        List<BattleTextTable.Data> result = new List<BattleTextTable.Data>();
        //���Ƴ�����
        List<int> deleteList = new List<int>();
        //��ȡ���ؽ��
        for (int i = 0; i < count; i++)
        {
            result.Add(bufferPool[type][i]);
            if (--bufferPool[type][i].useNum == 0)
                deleteList.Add(i);
        }
        foreach (int item in deleteList)
        {
            bufferPool[type].RemoveAt(item);
            numDic[type]--;
        }
        return result;
    }

    /// <summary>
    /// ��ȡָ�����еĿ�ʹ������/��������(������Ҫȷ��)
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private int GetTextCount(E_BattleTextType type)
    {
        return bufferPool[type].Count;
    }
}

#region �ɵ�BattleTextPoolBuffer

/*
public class BattleTextPoolBuffer:SingletonBase<BattleTextPoolBuffer>
{
    /// <summary>
    /// �����<�ı�����,����>
    /// </summary>
    private Dictionary<E_BattleTextType, List<BattleTextTable.Data>> bufferPool = new();
    /// <summary>
    /// �����Ϳ�����Ϣ������
    /// </summary>
    private Dictionary<E_BattleTextType, int> numDic = new() {
        { E_BattleTextType.Simple, 0 },
        { E_BattleTextType.Gruop, 0 },
        { E_BattleTextType.Connection, 0 },
        { E_BattleTextType.Frame, 0 },
    };

    /// <summary>
    /// ������ݵ�����
    /// </summary>
    /// <param name="battleTextId">�ı���Id</param>
    public void Add(int battleTextId)
    {
        BattleTextTable.Data battleInfo = BattleTextTable.Find(battleTextId);
        if (battleInfo == null)
        {
            Debug.LogWarning($"[BattleTextPoolBuffer] ���Լ��벻���ڵ�ս���ı�����{battleTextId}");
            return;
        }
        //��Ϣ�洢
        if (!bufferPool.ContainsKey(battleInfo.type))
            bufferPool.Add(battleInfo.type, new List<BattleTextTable.Data>());
        bufferPool[battleInfo.type].Add(battleInfo);
        //�������μ�¼
        numDic[battleInfo.type]++;
    }

    /// <summary>
    /// ��ָ���������ȡ��ָ����������
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<BattleTextTable.Data> Get(E_BattleTextType type,int count)
    {
        if (!bufferPool.ContainsKey(type) || GetCount(type) < count) 
        {
            Debug.LogWarning($"[BattleTextPoolBuffer] ���Ի�ȡ�����ڵ���Ϣ����Ϣ�������㹻{type}");
            return null;
        }
        //��Ž��
        List<BattleTextTable.Data> result = new List<BattleTextTable.Data>();
        //���Ƴ�����
        List<int> deleteList = new List<int>();
        //��ȡ���ؽ��
        for (int i = 0; i < count; i++) 
        {
            result.Add(bufferPool[type][i]);
            if (--bufferPool[type][i].useNum == 0)
                deleteList.Add(i);
        }
        foreach (int item in deleteList)
        {
            bufferPool[type].RemoveAt(item);
            numDic[type]--;
        }
        return result;
    }

    /// <summary>
    /// ��ȡָ�����еĿ�ʹ������/��������(������Ҫȷ��)
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetCount(E_BattleTextType type)
    {
        return bufferPool[type].Count;
    }

}
*/
#endregion

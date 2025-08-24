using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 生成的文本类型
/// </summary>
public enum E_BattleTextType 
{
    /// <summary>
    /// 独立成段:每个生成的文本自成一个自然段，需要缩进
    /// </summary>
    Simple = 1,
    /// <summary>
    /// 组合类型:独立生成文本,独立生成文本每3段合并为一个自然段，合并方式为直接相连
    /// </summary>
    Gruop,
    /// <summary>
    /// 段落内文本:三条文本作为一段,Frame类型所指定的框架拼接到草稿本中
    /// </summary>
    Connection,
    /// <summary>
    /// 段落框架
    /// </summary>
    Frame,
}

public class BattleTexManager : SingletonBase<BattleTexManager>
{
    /// <summary>
    /// 缓冲池<文本类型,对象>
    /// </summary>
    private Dictionary<E_BattleTextType, List<BattleTextTable.Data>> bufferPool = new();
    /// <summary>
    /// 各类型可用消息批次数
    /// </summary>
    private Dictionary<E_BattleTextType, int> numDic = new()
    {
        { E_BattleTextType.Simple, 0 },
        { E_BattleTextType.Gruop, 0 },
        { E_BattleTextType.Connection, 0 },
        { E_BattleTextType.Frame, 0 },
    };

    /// <summary>
    /// 文本生成
    /// </summary>
    /// <param name="id">局内文本表Id</param>
    /// <param name="generateType">生成类型</param>
    public void GenerateText(int id, E_BattleTextType generateType)
    {
        BattleTextTable.Data textInfo = BattleTextTable.Find(id);
        if (textInfo == null)
        {
            Debug.Log($"[文本生成器]在文本生成表找不到表项Id:{id}");
            return;
        }
        switch (generateType)
        {
            case E_BattleTextType.Simple:
                //将文本以独立自然段接入
                
                break;
            case E_BattleTextType.Gruop:
                //足够3条时,直接拼接到原来的文本后方
                AddTextData(id);
                if (GetTextCount(generateType) >= 3) 
                {
                    List<BattleTextTable.Data> textDatas = GetTextData(generateType, 3);
                    StringBuilder str = new StringBuilder();
                    str.Append(textDatas[0].content);
                    str.Append(textDatas[1].content);
                    str.Append(textDatas[2].content);
                    //接入文本
                    str.ToString();
                }
                break;
            case E_BattleTextType.Connection:
                AddTextData(id);
                //足够三条时,当有可用Frame类型,根据其拼接以独立自然段写入书中
                if (GetTextCount(E_BattleTextType.Connection) >= 3 && GetTextCount(E_BattleTextType.Frame) >= 1)   
                {
                    List<BattleTextTable.Data> textDatas = GetTextData(generateType, 3);
                    string part1 = textDatas[0].content;
                    string part2 = textDatas[1].content;
                    string part3 = textDatas[2].content;
                    string frame = GetTextData(E_BattleTextType.Frame)[0].content;
                    //独立自然段接入
                    string battleText = string.Format(frame, part1, part2, part3);
                }
                break;
            case E_BattleTextType.Frame:
                AddTextData(id);
                //当Connection足够3条,使用Frame为框架,拼接Connection类型以独立自然段写入书中
                if (GetTextCount(E_BattleTextType.Connection) >= 3 && GetTextCount(E_BattleTextType.Frame) >= 1)
                {
                    List<BattleTextTable.Data> textDatas = GetTextData(generateType, 3);
                    string part1 = textDatas[0].content;
                    string part2 = textDatas[1].content;
                    string part3 = textDatas[2].content;
                    string frame = GetTextData(E_BattleTextType.Frame)[0].content;
                    //独立自然段接入
                    string battleText = string.Format(frame, part1, part2, part3);
                }
                break;
        }
    }    
    
    /// <summary>
    /// 添加数据到缓冲
    /// </summary>
    /// <param name="battleTextId">文本表Id</param>
    private void AddTextData(int battleTextId)
    {
        BattleTextTable.Data battleInfo = BattleTextTable.Find(battleTextId);
        if (battleInfo == null)
        {
            Debug.LogWarning($"[BattleTextPoolBuffer] 尝试加入不存在的战斗文本数据{battleTextId}");
            return;
        }
        //消息存储
        if (!bufferPool.ContainsKey(battleInfo.type))
            bufferPool.Add(battleInfo.type, new List<BattleTextTable.Data>());
        bufferPool[battleInfo.type].Add(battleInfo);
        //可用批次记录
        numDic[battleInfo.type]++;
    }

    /// <summary>
    /// 从指定缓冲队列取出指定个数对象
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    private List<BattleTextTable.Data> GetTextData(E_BattleTextType type, int count = 1)
    {
        if (!bufferPool.ContainsKey(type) || GetTextCount(type) < count)
        {
            Debug.LogWarning($"[BattleTextPoolBuffer] 尝试获取不存在的信息或消息数量不足够{type}");
            return null;
        }
        //存放结果
        List<BattleTextTable.Data> result = new List<BattleTextTable.Data>();
        //待移除队列
        List<int> deleteList = new List<int>();
        //获取返回结果
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
    /// 获取指定队列的可使用数量/类型数量(需求需要确定)
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private int GetTextCount(E_BattleTextType type)
    {
        return bufferPool[type].Count;
    }
}

#region 旧的BattleTextPoolBuffer

/*
public class BattleTextPoolBuffer:SingletonBase<BattleTextPoolBuffer>
{
    /// <summary>
    /// 缓冲池<文本类型,对象>
    /// </summary>
    private Dictionary<E_BattleTextType, List<BattleTextTable.Data>> bufferPool = new();
    /// <summary>
    /// 各类型可用消息批次数
    /// </summary>
    private Dictionary<E_BattleTextType, int> numDic = new() {
        { E_BattleTextType.Simple, 0 },
        { E_BattleTextType.Gruop, 0 },
        { E_BattleTextType.Connection, 0 },
        { E_BattleTextType.Frame, 0 },
    };

    /// <summary>
    /// 添加数据到缓冲
    /// </summary>
    /// <param name="battleTextId">文本表Id</param>
    public void Add(int battleTextId)
    {
        BattleTextTable.Data battleInfo = BattleTextTable.Find(battleTextId);
        if (battleInfo == null)
        {
            Debug.LogWarning($"[BattleTextPoolBuffer] 尝试加入不存在的战斗文本数据{battleTextId}");
            return;
        }
        //消息存储
        if (!bufferPool.ContainsKey(battleInfo.type))
            bufferPool.Add(battleInfo.type, new List<BattleTextTable.Data>());
        bufferPool[battleInfo.type].Add(battleInfo);
        //可用批次记录
        numDic[battleInfo.type]++;
    }

    /// <summary>
    /// 从指定缓冲队列取出指定个数对象
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<BattleTextTable.Data> Get(E_BattleTextType type,int count)
    {
        if (!bufferPool.ContainsKey(type) || GetCount(type) < count) 
        {
            Debug.LogWarning($"[BattleTextPoolBuffer] 尝试获取不存在的信息或消息数量不足够{type}");
            return null;
        }
        //存放结果
        List<BattleTextTable.Data> result = new List<BattleTextTable.Data>();
        //待移除队列
        List<int> deleteList = new List<int>();
        //获取返回结果
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
    /// 获取指定队列的可使用数量/类型数量(需求需要确定)
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

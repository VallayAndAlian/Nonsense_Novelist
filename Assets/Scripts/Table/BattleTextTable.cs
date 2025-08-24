using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTextTable : MapTable<int, BattleTextTable.Data>
{
    public override string AssetName => "BattleTextData";
    /// <summary>
    /// 局内文本信息
    /// </summary>
    public class Data
    {
        //序号
        public int id;
        //文本类型:决定生成方式
        public E_BattleTextType type;
        //目标书本Id
        public int bookId;
        //文本的内容
        public string content;
        //可用次数
        public int useNum;
        //精彩分
        public int highLightScore;
        //浪漫分
        public int romanticScore;
        //恐怖分
        public int terrorScore;
        //冒险分
        public int adventureScore;
        //科幻分
        public int scienceScore;
    }

    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.id = reader.Read<int>();
        data.type = (E_BattleTextType)reader.Read<int>();
        data.bookId = reader.Read<int>();
        data.content = reader.Read<string>();
        data.useNum = reader.Read<int>();
        data.highLightScore = reader.Read<int>();
        data.romanticScore = reader.Read<int>();
        data.terrorScore = reader.Read<int>();
        data.adventureScore = reader.Read<int>();
        data.scienceScore = reader.Read<int>();
        // 反序列化数据
        return new KeyValuePair<int, Data>(data.id, data);
    }
}

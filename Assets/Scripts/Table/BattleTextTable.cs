using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTextTable : MapTable<int, BattleTextTable.Data>
{
    public override string AssetName => "BattleTextData";
    /// <summary>
    /// �����ı���Ϣ
    /// </summary>
    public class Data
    {
        //���
        public int id;
        //�ı�����:�������ɷ�ʽ
        public E_BattleTextType type;
        //Ŀ���鱾Id
        public int bookId;
        //�ı�������
        public string content;
        //���ô���
        public int useNum;
        //���ʷ�
        public int highLightScore;
        //������
        public int romanticScore;
        //�ֲ���
        public int terrorScore;
        //ð�շ�
        public int adventureScore;
        //�ƻ÷�
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
        // �����л�����
        return new KeyValuePair<int, Data>(data.id, data);
    }
}

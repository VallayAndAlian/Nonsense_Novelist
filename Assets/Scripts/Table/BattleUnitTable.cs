
using System.Collections.Generic;

public class BattleUnitTable : MapTable<int, BattleUnitTable.Data>
{
    public class Data
    {
        public int mKind;
        public string mAsset;
        public string mName;
        public float mAttack;
        public float mMaxHp;
        public float mDefense;
        public float mPsy;
        public float mSan;
        public List<int> mTalents = new List<int>();
    }

    public override string AssetName => "BattleUnitData";
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();

        data.mKind = reader.Read<int>();
        data.mAsset = reader.Read<string>();
        data.mName = reader.Read<string>();
        data.mAttack = reader.Read<float>();
        data.mMaxHp = reader.Read<float>();
        data.mDefense = reader.Read<float>();
        data.mPsy = reader.Read<float>();
        data.mSan = reader.Read<float>();

        int talentNum = reader.Read<int>();

        for (int i = 0; i < talentNum; i++)
        {
            int temp = reader.Read<int>();
            data.mTalents.Add(temp);
        }
        
        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}
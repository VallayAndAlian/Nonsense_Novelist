
using System.Collections.Generic;

public class BattleUnitTable : MapTable<int, BattleUnitTable.Data>
{
    public class Data
    {
        public int mKind;
        public BattleUnitType mInitType;
        public string mAsset;
        public string mName;
        public BookNameEnum mBook;
        public float mAttack;
        public float mMaxHp;
        public float mDefense;
        public float mPsy;
        public float mSan;
        public List<int> mTalents = new List<int>();
        public int mRoles;
        public List<int> mInitServants = new List<int>();

        public class TreeData
        {
            public int mEffect;
            public List<int> mCon=new List<int>();
        }


        public List<TreeData> mTrees = new List<TreeData>();
    }

    public override string AssetName => "BattleUnitData";
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();

        data.mKind = reader.Read<int>();
        data.mInitType = (BattleUnitType)reader.Read<int>();
        data.mAsset = reader.Read<string>();
        data.mName = reader.Read<string>();
        data.mBook = (BookNameEnum)reader.Read<int>();
        data.mAttack = reader.Read<float>();
        data.mMaxHp = reader.Read<float>();
        data.mDefense = reader.Read<float>();
        data.mPsy = reader.Read<float>();
        data.mSan = reader.Read<float>();
        data.mTalents.AddRange(reader.ReadVec<int>());
        data.mRoles = reader.Read<int>();
        data.mInitServants.AddRange(reader.ReadVec<int>());
        for (int _temp = 0; _temp < 6; _temp++)
        {
            Data.TreeData treeData = new Data.TreeData();
            treeData.mEffect=reader.Read<int>();
            treeData.mCon=reader.ReadVec<int>();
            data.mTrees.Add(treeData);
        }



        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}
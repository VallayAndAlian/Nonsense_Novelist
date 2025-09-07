using System.Collections.Generic;
using System.Diagnostics;

public class UnitLevelUpTable : MapTable<int, UnitLevelUpTable.Data>
{
    public class Data
    {
        public class CareerData
        {
            public BattleUnitCareerType mType;
            public float mWeight;
        }
        public class AttrData
        {
            public AttributeType mType;
            public float mAddValue;
        }
        public int mKind;
        public BattleUnitCareerAnchor mAnchor;
        public List<CareerData> mCareerDatas = new List<CareerData>();
        public List<AttrData> mAttrDatas = new List<AttrData>();
    }
    public override string AssetName => "UnitLevelUpData";

    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        data.mAnchor = (BattleUnitCareerAnchor)reader.Read<int>();
        int attrNum = reader.Read<int>();
        if (attrNum == 0)
        {
            reader.MarkReadInvalid();
            return default;
        }
        for (int i = 0; i < attrNum; i++)
        {
            Data.AttrData attrData = new Data.AttrData();
            attrData.mType = (AttributeType)reader.Read<int>();
            attrData.mAddValue = reader.Read<float>();

            data.mAttrDatas.Add(attrData);
        }
        int careerNum = reader.Read<int>();
        if (careerNum == 0)
        {
            reader.MarkReadInvalid();
            return default;
        }
        for (int i = 0; i < careerNum; i++)
        {
            Data.CareerData careerData = new Data.CareerData();
            careerData.mType =(BattleUnitCareerType) reader.Read<int>();
            careerData.mWeight= reader.Read<float>();

            data.mCareerDatas.Add(careerData);
        }
        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}
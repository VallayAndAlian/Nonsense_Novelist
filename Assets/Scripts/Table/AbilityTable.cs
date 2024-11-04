
using System.Collections.Generic;

public class AbilityTable : MapTable<int, AbilityTable.Data>
{
    public class Data
    {
        public int mKind;
        public AbilityType mType;
        public string mName;
        public string mDesc;
        public int mMaxStackCount;

        public class CustomParam
        {
            public string mKey;
            public List<float> mValues;
        }

        public List<CustomParam> mCustomParams;
    }

    public override string AssetName => "AbilityData";
    
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        data.mType = (AbilityType)reader.Read<int>();
        data.mName = reader.Read<string>();
        data.mDesc = reader.Read<string>();
        
        data.mMaxStackCount = reader.Read<int>();

        int paramNum = reader.Read<int>();
        if ((paramNum / 2) != 0)
        {
            reader.MarkReadInvalid();
            return default;
        }

        data.mCustomParams = new List<Data.CustomParam>();
        for (int i = 0; i < paramNum / 2; i++)
        {
            Data.CustomParam param = new Data.CustomParam
            {
                mKey = reader.Read<string>(),
                mValues = reader.ReadVec<float>()
            };

            data.mCustomParams.Add(param);
        }
            
        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}
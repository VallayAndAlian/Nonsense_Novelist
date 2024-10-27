
using System.Collections.Generic;

public class AbilityTable : MapTable<int, AbilityTable.Data>
{
    public class Data
    {
        public int mKind;
        public AbilityType mType;
        public string mName;
        public string mDesc;
    }

    public override string AssetName => "AbilityData";
    
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        data.mType = (AbilityType)reader.Read<int>();
        data.mName = reader.Read<string>();
        data.mDesc = reader.Read<string>();

        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}
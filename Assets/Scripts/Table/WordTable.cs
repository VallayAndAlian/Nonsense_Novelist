

using System.Collections.Generic;

public enum WordType
{
    Undefined = 0,
    Property,              //特性
    Noun,                  //名词
    Verb,                  //动词
    Adjective,             //形容词
}

public enum WordTag
{
    Undefined = 0,
}

public class WordTable : MapTable<int, WordTable.Data>
{
    public class Data
    {
        public int mKind;
        public WordType mType;
        public string mDesc;
        public float mDuration;
        public List<int> mTags;
        public List<int> mAbilities;
    }

    public override string AssetName => "WordData";
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();

        data.mKind = reader.Read<int>();
        data.mType = (WordType)reader.Read<int>();
        data.mDesc = reader.Read<string>();

        data.mTags = reader.ReadVec<int>();
        data.mAbilities = reader.ReadVec<int>();

        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}
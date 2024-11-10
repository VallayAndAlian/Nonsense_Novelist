
using System.Collections.Generic;

public enum CardShootType
{
    None=0,
    Ghost=1,//通感
    Split=2,//排比
    Stronger=3,//递进
    Reveal=4,//暗喻
    Copy=5,//夸张
}

public enum CardType
{
    Other = 0,
    Verb=1,
    Adj=2,
    Noun=3
}

public class CardTable : MapTable<int, CardTable.Data>
{
    public class Data
    {
        public int mKind;
        public CardType mType;
        public string mAsset;
        public string mName;
        public BookNameEnum mBook;
        public List<int> mShootType;
        public int mUseTimes;
        public List<int> mSkills;

    }

    public override string AssetName => "CardData";
    
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        data.mType = (CardType)reader.Read<int>();
        data.mAsset = reader.Read<string>();
        data.mName = reader.Read<string>();
        data.mBook = (BookNameEnum)reader.Read<int>();
        data.mShootType.AddRange(reader.ReadVec<int>());
        data.mUseTimes = reader.Read<int>();
        data.mSkills.AddRange(reader.ReadVec<int>());

        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}
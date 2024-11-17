
public class BattleConfig : JsonTable<BattleConfig.Data>
{
    [System.Serializable]
    public class WordConfig
    {
        public int maxVerbNum = 3;
    }
    
    [System.Serializable]
    public class Data
    {
        public WordConfig word;
    }

    public override string AssetName => "BattleConfig";
}
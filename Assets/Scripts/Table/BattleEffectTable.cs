
using System;
using System.Collections.Generic;


public class BattleEffectTable : MapTable<int, BattleEffectTable.Data>
{
    public class Data
    {
        public int mKind;
        public EffectType mType;
        public string mName;
        public string mDesc;

        public float mTickInterval;
        
        public EffectDurationRule mDurationRule;
        public float mDuration;
        
        public EffectStackRule mStackRule;
        public EffectStackDurationRule mStackDurationRule;
        public int mMaxStackCount;

        public bool mRemoveOnCombatEnd;
        public bool mCanBePurged;
        public bool mPositive;

        public Status mApplyStatus;
        public List<AttributeModifier> mModifiers = new List<AttributeModifier>();
        
        public Dictionary<string, CustomParam> mCustomParams = new Dictionary<string, CustomParam>();
    }

    public override string AssetName => "BattleEffectData";
    
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();
        data.mType = (EffectType)reader.Read<int>();
        data.mName = reader.Read<string>();
        data.mDesc = reader.Read<string>();
        
        data.mTickInterval = reader.Read<float>();
        
        data.mDurationRule = (EffectDurationRule)reader.Read<int>();
        data.mDuration = reader.Read<float>();
        
        data.mStackRule = (EffectStackRule)reader.Read<int>();
        data.mStackDurationRule = (EffectStackDurationRule)reader.Read<int>();
        data.mMaxStackCount = reader.Read<int>();
        
        data.mRemoveOnCombatEnd = reader.Read<bool>();
        data.mCanBePurged = reader.Read<bool>();
        data.mPositive = reader.Read<bool>();
        
        data.mApplyStatus = (Status)reader.Read<Int64>();

        if (!ReaderUtils.ParseParams(reader, data.mModifiers))
            return default;
        
        if (!ReaderUtils.ParseCustomParams(reader, data.mCustomParams))
            return default;
        
        
        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}
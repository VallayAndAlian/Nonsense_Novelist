

using System.Collections.Generic;
using System.Diagnostics;

public class AbilityTemplateTable : MapTable<int, AbilityTemplateTable.Data>
{
    public class Data
    {
        public class ModuleData
        {
            public int mType;
            public List<float> mParams=new List<float>();
        }

        public int mKind;
        public ModuleData mTriggerData = new ModuleData();
        public ModuleData mSelectorData = new ModuleData();
        public List<ModuleData> mEffectApplyDataList = new List<Data.ModuleData>();
    }

    public override string AssetName => "AbilityTemplateData";
    
    protected override KeyValuePair<int, Data> ParseMapEntry(TokenReader reader)
    {
        Data data = new Data();
        data.mKind = reader.Read<int>();


        data.mTriggerData.mType = reader.Read<int>();
        int num = reader.Read<int>();
        for (int i = 0; i < num; i++)
        {
            data.mTriggerData.mParams.AddRange(reader.ReadVec<float>());
        }
        
        
        data.mSelectorData.mType = reader.Read<int>();
        num = reader.Read<int>();
        for (int i = 0; i < num; i++)
        {
            data.mSelectorData.mParams.AddRange(reader.ReadVec<float>());
        }
        

        int effectNum = reader.Read<int>();
        if (effectNum == 0)
        {
            reader.MarkReadInvalid();
            return default;
        }
        for (int i = 0; i < effectNum; i++)
        {
            Data.ModuleData moduleData = new Data.ModuleData();
            moduleData.mType = reader.Read<int>();
            
            num = reader.Read<int>();
            for (int j = 0; j < num; j++)
            {
                moduleData.mParams.AddRange(reader.ReadVec<float>());
            }
            
            data.mEffectApplyDataList.Add(moduleData);
        }
        

        return new KeyValuePair<int, Data>(data.mKind, data);
    }
}
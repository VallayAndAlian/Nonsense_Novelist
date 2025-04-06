
using System;
using System.Collections.Generic;

public class WordEntry
{
    public WordType mType;
    public float mApplyTime = 0;
    public float mEndTime = 0;
    
    public WordTable.Data mData = null;
    public List<AbilityBase> mAbilities = null;
}

public class WordComponent : UnitComponent
{
    protected Dictionary<WordType, List<WordEntry>> mWordEntries = new Dictionary<WordType, List<WordEntry>>();
    protected List<WordEntry> mAdjectiveWords = null;

    public List<WordEntry> GetWordsByType(WordType wt)
    {
        return mWordEntries.TryGetValue(wt, out var words) ? words : null;
    }

    protected override void OnRegistered()
    {
        mWordEntries.Clear();
        foreach (WordType wt in Enum.GetValues(typeof(WordType)))
        {
            if (wt != WordType.Undefined)
            {
                mWordEntries.Add(wt, new List<WordEntry>());
            }
        }

        mAdjectiveWords = GetWordsByType(WordType.Adjective);
    }

    public override void Start()
    {
        foreach (var abi in mOwner.Data.mTalents)
        {
            AddWord(abi);
        }
        
        AddWord(mOwner.Data.mRoles);
    }

    public override void Update(float deltaTime)
    {
        List<WordEntry> Removed = new List<WordEntry>();
        foreach (var w in mAdjectiveWords)
        {
            if (w.mEndTime > Owner.Battle.Now)
            {
                Removed.Add(w);
            }
        }

        foreach (var w in Removed)
        {
            ClearAbilities(w);
            mAdjectiveWords.Remove(w);
        }
    }

    public WordEntry AddWord(int wordKind)
    {
        var wordData = WordTable.Find(wordKind);
        if (wordData == null)
            return null;

        var words = GetWordsByType(wordData.mType);
        if (words == null)
            return null;

        WordEntry entry = new WordEntry();

        entry.mType = wordData.mType;
        entry.mApplyTime = Owner.Battle.Now;
        entry.mData = wordData;

        switch (wordData.mType)
        {
            case WordType.Property:
            case WordType.Noun:
                break;
            
            case WordType.Verb:
                if (words.Count > 0 && mWordEntries.Count >= BattleConfig.mData.word.maxVerbNum)
                {
                    ClearAbilities(words[0]);
                    
                    words.RemoveAt(0);
                }
                
                break;
            
            case WordType.Adjective:
                entry.mEndTime = entry.mApplyTime + wordData.mDuration;
                break;
            
            default:
                break;
        }
        
        words.Add(entry);

        entry.mAbilities = new List<AbilityBase>();
        
        foreach (var kind in wordData.mAbilities)
        {
            var abi = Owner.AbilityAgent.RegisterAbility(kind);
            if (abi != null)
            {
                entry.mAbilities.Add(abi);
            }
        }

        return entry;
    }

    public void RemoveWord(WordEntry we)
    {
        if (we?.mData == null)
            return;
        
        var words = GetWordsByType(we.mData.mType);
        if (words == null)
            return;
        
        if (!words.Contains(we)) 
            return;

        ClearAbilities(we);

        words.Remove(we);
    }

    public void ClearAbilities(WordEntry we)
    {
        foreach (var abi in we.mAbilities)
        {
            Owner.AbilityAgent.RemoveAbility(abi);
        }
    }
}
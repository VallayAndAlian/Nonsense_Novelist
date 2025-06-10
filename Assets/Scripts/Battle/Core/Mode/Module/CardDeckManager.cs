using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : BattleModule
{
    
    
    protected WordTable.Data mCurrentCard = null;
    protected Queue<WordTable.Data> mPreviewCards = new Queue<WordTable.Data>();

    protected bool mHasShuffleDeck = false;
    
    public class LoadSlot
    {
        public int mIndex = 0;
        public float mLoadTimer = 0; 
        public WordTable.Data mWord = null;
    }

    protected List<LoadSlot> mLoadSlots = new List<LoadSlot>();
    public List<LoadSlot> LoadSlots => mLoadSlots;

    // 未抽取的牌库
    protected List<WordTable.Data> mDeck = new List<WordTable.Data>();

    // 弃牌堆
    protected List<WordTable.Data> mDiscardPile = new List<WordTable.Data>();

    protected int mNextSlot = 0;
    
    protected int mMaxPreviewCount = 0;

    public int MaxPreviewCount
    {
        get => mMaxPreviewCount;

        set
        {
            mMaxPreviewCount = value;
            if (mMaxPreviewCount >= mLoadSlots.Count)
            {
                for (int i = mLoadSlots.Count; i < mMaxPreviewCount; ++i)
                {
                    mLoadSlots.Add(new LoadSlot()
                    {
                        mLoadTimer = BattleConfig.mData.word.loadSec,
                        mWord = !mHasShuffleDeck ? LoadWord() : null,
                    });
                }
            }
            else
            {
                for (int i = mLoadSlots.Count - 1; i >= mMaxPreviewCount; --i)
                {
                    mLoadSlots.RemoveAt(i);
                }
            }
        }
    }

    public override void Init()
    {
        AddInitCardDeck();
    }

    public override void Update(float deltaSec)
    {
        if (!Battle.BattlePhase.IsCombat)
            return;
        
        foreach (var slot in mLoadSlots)
        {
            if (slot.mWord != null)
                continue;

            slot.mLoadTimer -= deltaSec;
            if (slot.mLoadTimer <= 0)
            {
                slot.mLoadTimer = BattleConfig.mData.word.loadSec;
                slot.mWord = LoadWord();
            }
        }
    }

    public override void OnEnterCombatPhase()
    {
        if (!mHasShuffleDeck)
        {
            ShuffleDeck();

            MaxPreviewCount = 2;
            mHasShuffleDeck = true;
        }
    }

    protected override void OnDisposed()
    {
        base.OnDisposed();

        mCurrentCard = null;
        mPreviewCards.Clear();
        mDeck.Clear();
        mDiscardPile.Clear();
    }
    

    /// <summary>
    ///初始卡牌
    /// </summary>
    private void AddInitCardDeck()
    {
        foreach (var word in WordTable.DataList.Values)
        {
            if (!word.mForbidden)
            {
                AddCardToDeck(word);
            }
        }
    }

    /// <summary>
    /// 洗牌操作
    /// </summary>
    private void ShuffleDeck()
    {
        for (int i = 0; i < mDeck.Count; i++)
        {
            int randomIndex = Random.Range(0, mDeck.Count);
            (mDeck[i], mDeck[randomIndex]) = (mDeck[randomIndex], mDeck[i]);
        }
    }

    private WordTable.Data LoadWord()
    {
        if (mDeck.Count == 0)
        {
            RefillDeckFromDiscardPile();
        }

        var word = mDeck[^1];
        mDeck.RemoveAt(mDeck.Count - 1);
        
        return word;
    }

    private void DrawCurrentAndPreviewCards()
    {
        if (mCurrentCard == null)
        {
            DrawCurrentCard();
        }

        while (mPreviewCards.Count < mMaxPreviewCount)
        {
            if (mDeck.Count == 0)
            {
                RefillDeckFromDiscardPile();
            }

            mPreviewCards.Enqueue(mDeck[0]);
            mDeck.RemoveAt(0);
        }
    }

    /// <summary>
    /// 抽取当前卡牌
    /// </summary>
    protected void DrawCurrentCard()
    {
        if (mPreviewCards.Count > 0)
        {
            mCurrentCard = mPreviewCards.Dequeue();
        }
        else
        {
            if (mDeck.Count == 0)
            {
                RefillDeckFromDiscardPile();
            }

            if (mDeck.Count > 0)
            {
                mCurrentCard = mDeck[0];
                mDeck.RemoveAt(0);
            }
        }
        
        var infoCard = GameObject.Find("WordInformation");
        if (infoCard != null)
        {
            infoCard.GetComponent<WordInformation>().ChangeInformation(mCurrentCard);
        }
    }

    /// <summary>
    /// 使用当前卡牌
    /// </summary>
    public void UseCurrentCard()
    {
        if (mCurrentCard == null)
        {
            Debug.Log("currentCard == null");
            return;
        }

        mDiscardPile.Add(mCurrentCard);
        mCurrentCard = null;
        DrawCurrentAndPreviewCards();
    }

    /// <summary>
    /// 弃牌堆加入牌库并洗牌
    /// </summary>
    private void RefillDeckFromDiscardPile()
    {
        if (mDiscardPile.Count == 0)
        {
            Debug.LogError("discardPile.Count == 0");
            return;
        }

        mDeck.AddRange(mDiscardPile);
        mDiscardPile.Clear();
        ShuffleDeck();
    }

    /// <summary>
    /// 增加新卡牌到牌库
    /// </summary>
    public void AddCardToDeck(WordTable.Data cardData)
    {
        if (cardData is { mForbidden: false })
        {
            mDeck.Add(cardData);
        }
    }

    /// <summary>
    /// 移除某张卡牌
    /// </summary>
    public void RemoveCardFromGame(WordTable.Data cardData)
    {
        mDeck.Remove(cardData);
        mDiscardPile.Remove(cardData);

        // 如果当前卡牌被移除，立即更新当前卡牌
        if (mCurrentCard == cardData)
        {
            UseCurrentCard();
        }
    }

    public WordTable.Data GetCurrentCard()
    {
        if (mCurrentCard == null)
        {
            Debug.Log("currentCard == null");
        }

        return mCurrentCard;
    }

    public Queue<WordTable.Data> GetPreviewCards()
    {
        return new Queue<WordTable.Data>(mPreviewCards);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : BattleModule
{
    private WordTable.Data currentCard;
    private Queue<WordTable.Data> previewCards = new Queue<WordTable.Data>();

    // 未抽取的牌库
    private List<WordTable.Data> deck = new List<WordTable.Data>();

    // 弃牌堆
    private List<WordTable.Data> discardPile = new List<WordTable.Data>();
    public int maxPreviewCount = 2;

    public override void Init()
    {
        base.Init();

       AddInitCardDeck();
        ShuffleDeck();
        DrawCurrentAndPreviewCards();
    }

    /// <summary>
    ///初始卡牌
    /// </summary>
    private void AddInitCardDeck()
    {
        AddCardToDeck(WordTable.Find(1));AddCardToDeck(WordTable.Find(1));
        AddCardToDeck(WordTable.Find(1));AddCardToDeck(WordTable.Find(1));
    }

    /// <summary>
    /// 洗牌操作
    /// </summary>
    private void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            WordTable.Data temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    private void DrawCurrentAndPreviewCards()
    {
        if (currentCard == null)
        {
            DrawCurrentCard();
        }

        while (previewCards.Count < maxPreviewCount)
        {
            if (deck.Count == 0)
            {
                RefillDeckFromDiscardPile();
            }

            previewCards.Enqueue(deck[0]);
            deck.RemoveAt(0);
        }
    }

    /// <summary>
    /// 抽取当前卡牌
    /// </summary>
    private void DrawCurrentCard()
    {
        if (previewCards.Count > 0)
        {
            currentCard = previewCards.Dequeue();
        }
        else
        {
            if (deck.Count == 0)
            {
                RefillDeckFromDiscardPile();
            }

            if (deck.Count > 0)
            {
                currentCard = deck[0];
                deck.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// 使用当前卡牌
    /// </summary>
    public void UseCurrentCard()
    {
        if (currentCard == null)
        {
            Debug.Log("currentCard == null");
            return;
        }

        discardPile.Add(currentCard);
        currentCard=null;
        DrawCurrentCard();
        DrawCurrentAndPreviewCards();
    }

    /// <summary>
    /// 弃牌堆加入牌库并洗牌
    /// </summary>
    private void RefillDeckFromDiscardPile()
    {
        if (discardPile.Count == 0)
        {
            Debug.Log("discardPile.Count == 0");
            return;
        }

        deck.AddRange(discardPile);
        discardPile.Clear();
        ShuffleDeck();
    }

    /// <summary>
    /// 增加新卡牌到牌库
    /// </summary>
    public void AddCardToDeck(WordTable.Data cardData)
    {
        deck.Add(cardData);
 
    }

    /// <summary>
    /// 移除某张卡牌
    /// </summary>
    public void RemoveCardFromGame(WordTable.Data cardData)
    {
        deck.Remove(cardData);
        discardPile.Remove(cardData);

        // 如果当前卡牌被移除，立即更新当前卡牌
        if (currentCard == cardData)
        {
            UseCurrentCard();
        }
    }

    public WordTable.Data GetCurrentCard()
    {
        if (currentCard == null)
        {
            Debug.Log("currentCard == null");
        }
        return currentCard;
    }

    public Queue<WordTable.Data> GetPreviewCards()
    {
        return new Queue<WordTable.Data>(previewCards);
    }


    public override void Update(float deltaSec)
    {
        base.Update(deltaSec);
    }
    public override void Dispose()
    {
        base.Dispose();

        currentCard = null;
        previewCards.Clear();
        deck.Clear();
        discardPile.Clear();
    }
}

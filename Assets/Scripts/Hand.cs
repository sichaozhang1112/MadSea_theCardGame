using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : UICardsPile
{
    //[SerializeField] List<Card> cards = new List<Card>();

    public int GetCount()
    {
        return cards.Count;
    }
    public void AddCard(Card card)
    {
        cards.Add(card);
    }
    public Card GetCard(int index)
    {
        return cards[index];
    }

    public void DrawCards(int count)
    {
        Deck deck = MainManager.instance.GetDeck();
        for (int i = 0; i < count; i++)
        {
            Card card = deck.DrawCard();
            if (card is null) break;
            else this.AddCard(card);
        }
    }

    public void PlayCard(int index)
    {
        Card playedOne = cards[index];
        cards.Remove(playedOne);
        playedOne.OnPlayed(); // the logic of where the played cards go to should be implemented in cards behavior
    }

    public void ExileCard(int index)
    {
        Card playedOne = cards[index];
        cards.Remove(playedOne);
        playedOne.OnExile(); // the logic of where the played cards go to should be implemented in cards behavior
    }

    public void DiscardAll()
    {
        DiscardPile dp = MainManager.instance.GetDiscardPile();
        foreach (var item in cards)
        {
            dp.AddCard(item);
        }

        cards.Clear();
    }

    public void DiscardCard(int index)
    {
        Card playedOne = cards[index];
        DiscardPile dp = MainManager.instance.GetDiscardPile();
        dp.AddCard(playedOne);
        cards.Remove(playedOne);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    RefreshUI();
        //}
    }
}

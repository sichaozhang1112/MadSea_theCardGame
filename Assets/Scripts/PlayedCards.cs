using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayedCards : UICardsPile
{
    //List<Card> cards = new List<Card>();
    public void AddCard(Card card)
    {
        cards.Add(card);
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

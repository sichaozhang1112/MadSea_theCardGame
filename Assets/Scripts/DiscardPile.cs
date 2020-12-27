using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    List<Card> cards = new List<Card>();
    public int GetCount()
    {
        return cards.Count;
    }
    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public List<Card> returnAll()
    {
        List<Card> returnCards = new List<Card>();
        foreach (var item in cards)
        {
            returnCards.Add(item);
        }
        cards.Clear();

        return returnCards;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

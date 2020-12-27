using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] List<Card> cards = new List<Card>();

    public int GetCount()
    {
        return cards.Count;
    }

    public void ReturnDiscards()
    {
        List<Card> discards = MainManager.instance.GetDiscardPile().returnAll(); // Discard cards called discard! I'm a genius!
        foreach (var item in discards)
        {
            cards.Add(item);
        }
        Shuffle();
    }
    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            ReturnDiscards();
        }
        if (cards.Count > 0)
        {
            Card drawed = cards[0];
            cards.Remove(drawed);

            return drawed;
        }
        else
        {
            return null;
        }
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void Shuffle()
    {
        Random ran = new Random();
        int count = cards.Count;

        for(int i = 0; i<count; i++)
        {
            int index = Random.Range(i, count-1);
            Card tmp = cards[index];
            cards[index] = cards[i];
            cards[i] = tmp;
        }
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

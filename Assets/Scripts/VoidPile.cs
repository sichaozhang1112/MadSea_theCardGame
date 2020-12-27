using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPile : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

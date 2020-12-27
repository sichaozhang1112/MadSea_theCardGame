using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleField : UICardsPile
{
    [SerializeField] int size = 6;
    //[SerializeField] GameObject UIcardTemplate;
    //List<Card> cards = new List<Card>();
    //List<GameObject> UIcards = new List<GameObject>();

    //public void AddCard(Card card)
    //{
    //    cards.Add(card);
    //}
    public void DrawCard(int targetIndex)
    {
        BattleDeck deck = MainManager.instance.GetBattleDeck();
        Card card = deck.DrawCard();
        if (card is null) { Debug.LogError("empty battle deck!!"); cards.RemoveAt(targetIndex); }
        else cards[targetIndex] = card;
    }

    public Card GetCard(int index)
    {
        return cards[index];
    }

    public void SetCard(int index, Card card)
    {
        // this should be used only for cursed convert
        cards[index] = card;
    }

    public List<Card> GetCards()
    {
        return cards;
    }

    public void LeaveCard(int index)
    {
        DrawCard(index);
    }

    //public void ClearUICards()
    //{
    //    foreach (var item in UIcards)
    //    {
    //        Destroy(item);
    //    }
    //}

    //public void RefreshUI()
    //{
    //    float width = 80;
    //    float gap = 10;

    //    ClearUICards();

    //    float fullSize = width * size + gap * (size - 1);
    //    float start = - fullSize / 2.0f;
    //    for (int i = 0; i < size; i++)
    //    {
    //        Debug.Log("generate UIcard");
    //        GameObject newUICard = GameObject.Instantiate(UIcardTemplate, this.transform);
    //        newUICard.GetComponent<UICard>().Init(cards[i]);
    //        newUICard.GetComponent<RectTransform>().anchoredPosition = new Vector2(start + i * (width + gap) + width / 2.0f, 0);
    //        UIcards.Add(newUICard);
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        size = PlayerPrefs.GetInt("FieldSize");
        BattleDeck deck = MainManager.instance.GetBattleDeck();
        deck.Init();
        deck.Shuffle();
        for (int i = 0; i < size; i++)
        {
            Card card = deck.DrawCard();
            if (card is null) { Debug.LogError("empty battle deck!!"); }
            else cards.Add(card);
        }
        RefreshUI();
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

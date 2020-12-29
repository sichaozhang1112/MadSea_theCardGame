using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDeck : MonoBehaviour
{
    [SerializeField] List<Card> cards = new List<Card>();
    [SerializeField] List<GameObject> cardPrefabs = new List<GameObject>();
    [SerializeField] List<int> cardPrefabsNum = new List<int>();
    [SerializeField] GameObject cardsInstance;
    public int GetCount()
    {
        return cards.Count;
    }
    public Card DrawCard()
    {
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
    public void Shuffle()
    {
        int count = cards.Count;
        int index_seaLoad = 0;
        int index;
        Card tmp;

        for (int i = 1; i < count; i++)
        {
            index = Random.Range(i, count - 1);
            tmp = cards[index];
            cards[index] = cards[i];
            cards[i] = tmp;
        }

        if (PlayerPrefs.GetInt("Difficult") >= 3)
        {
            index = Random.Range(0, 80);
        }else if (PlayerPrefs.GetInt("Difficult") >= 2)
        {
            index = Random.Range(30, count);
        }
        else
        {
            index = Random.Range(50, count);
        }
        tmp = cards[index];
        cards[index] = cards[index_seaLoad];
        cards[index_seaLoad] = tmp;
    }

    public void Init()
    {
        for (int i = 0; i < cardPrefabs.Count; i++)
        {
            int cardNum = cardPrefabsNum[i];
            if (cardNum <= 0) cardNum = 1;
            for (int j = 0; j < cardPrefabsNum[i]; j++)
            {
                GameObject go;
                if (true)
                {
                    go = GameObject.Instantiate(cardPrefabs[i], this.transform);
                }
                else
                {
                    // todo: run second time failed as cardsInstance will be null. I have no idea yet.
                    go = GameObject.Instantiate(cardPrefabs[i], cardsInstance.transform);
                }
                cards.Add(go.GetComponent<Card>());
            }
        }
        Shuffle();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Init();
        //}
    }
}

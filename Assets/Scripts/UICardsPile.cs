using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardsPile : MonoBehaviour
{
    [SerializeField] GameObject uiCardTemplate;
    [SerializeField] protected List<Card> cards = new List<Card>();
    List<GameObject> uiCards = new List<GameObject>();
    List<Vector3> listPos = new List<Vector3>();

    [SerializeField] string pileTag;
    int lastClickedIndex;

    public void OnCardClick(int index)
    {
        lastClickedIndex = index;
        MainManager.instance.OnCardClicked(pileTag, index);
        //RefreshUI();
    }
    public int GetLastClicked() { return lastClickedIndex; }
    public void ClearUICards()
    {
        foreach (var item in uiCards)
        {
            Destroy(item);
        }
    }

    //public Vector3 GetCardPos(int index)
    //{
    //    return uiCards[index].transform.position;
    //}

    public void RefreshUI()
    {
        int size = cards.Count;
        float width = 80;
        float gap = 10;

        ClearUICards();

        float fullSize = width * size + gap * (size - 1);
        float start = -fullSize / 2.0f;
        for (int i = 0; i < size; i++)
        {
            GameObject newUICard = GameObject.Instantiate(uiCardTemplate, this.transform);
            newUICard.GetComponent<UICard>().Init(cards[i], this, i);
            newUICard.GetComponent<RectTransform>().anchoredPosition = new Vector2(start + i * (width + gap) + width / 2.0f, 0);
            uiCards.Add(newUICard);
        }
    }
}

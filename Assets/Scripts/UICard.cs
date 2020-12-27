using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardAddonUIType
{
    none,
    charge,
    curse
}

public class UICard : MonoBehaviour
{
    [SerializeField] Text cardname;
    [SerializeField] Text cost;
    [SerializeField] Text detail;
    [SerializeField] GameObject addOnUIBar;
    [SerializeField] GameObject addOnUISlot;

    [SerializeField] float addOnUISlotGap = 15f;
    [SerializeField] float addOnUISlotGapY = 15f;
    [SerializeField] int addOnUISlotNumPerRow = 5;

    [SerializeField] GameObject FangAnim;
    [SerializeField] GameObject CursedAnim;
    UICardsPile father;
    int cardIndex;
    public void Init(Card card, UICardsPile iFather, int index)
    {
        cardname.text = card.getCardName();
        cost.text = card.getCost().ToString();
        if(card.getCostType() == CostType.attack)
        {
            cost.color = new Color(1f, 0f, 0f);
        }
        else
        {
            cost.color = new Color(0f, 0f, 0f);
        }
        detail.text = card.getDetail();

        father = iFather;
        cardIndex = index;

        CardAddonUIType cauType;int value1;int value2;
        card.GetAddonUI(out cauType, out value1, out value2);
        float offset = 0;
        float offsetY = 0;

        switch (cauType)
        {
            case CardAddonUIType.none:
                break;
            case CardAddonUIType.charge:
                for (int i = 0; i < value1; i++)
                {
                    offset = (i % addOnUISlotNumPerRow) * addOnUISlotGap;
                    offsetY = -(int)(i / addOnUISlotNumPerRow) * addOnUISlotGapY;
                    GameObject newSlot = GameObject.Instantiate(addOnUISlot, addOnUIBar.transform);
                    newSlot.GetComponent<Image>().color = new Color(1f, 0f, 0f);
                    newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, offsetY);
                    
                }
                for (int i = value1; i < value2; i++)
                {
                    offset = (i % addOnUISlotNumPerRow) * addOnUISlotGap;
                    offsetY = -(int)(i / addOnUISlotNumPerRow) * addOnUISlotGapY;
                    GameObject newSlot = GameObject.Instantiate(addOnUISlot, addOnUIBar.transform);
                    newSlot.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
                    newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, offsetY);
                }
                break;
            case CardAddonUIType.curse:
                for (int i = 0; i < value1; i++)
                {
                    offset = (i % addOnUISlotNumPerRow) * addOnUISlotGap;
                    offsetY = -(int)(i / addOnUISlotNumPerRow) * addOnUISlotGapY;
                    GameObject newSlot = GameObject.Instantiate(addOnUISlot, addOnUIBar.transform);
                    newSlot.GetComponent<Image>().color = new Color(1f, 0f, 1f);
                    newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, offsetY);
                }
                for (int i = value1; i < value2; i++)
                {
                    offset = (i % addOnUISlotNumPerRow) * addOnUISlotGap;
                    offsetY = -(int)(i / addOnUISlotNumPerRow) * addOnUISlotGapY;
                    GameObject newSlot = GameObject.Instantiate(addOnUISlot, addOnUIBar.transform);
                    newSlot.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
                    newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, offsetY);
                }
                break;
            default:
                break;
        }

        if (card.GetLastAttacked())
        {
            GameObject go = GameObject.Instantiate(FangAnim, this.transform);
            Destroy(go, 35f / 60f);
        }

        if (card.GetLastCursed())
        {
            Debug.Log("curse anim");
            //CursedAnim.transform.position = this.transform.position;
            GameObject go = GameObject.Instantiate(CursedAnim, this.transform);
            Destroy(go, 30f / 60f);

        }
    }

    public void OnClick()
    {
        father.OnCardClick(cardIndex);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePack : HeroCard
{
    [SerializeField] GameObject targetCard;
    [SerializeField] int num;
    [SerializeField] PileType where;
    override public void OnPlayed()
    {
        GameObject ci = MainManager.instance.GetCardsInstance();
        GameObject go = GameObject.Instantiate(targetCard, ci.transform);
        switch (where)
        {
            case PileType.discard:
                MainManager.instance.GetDiscardPile().AddCard(go.GetComponent<Card>());
                break;
            case PileType.hand:
                MainManager.instance.GetHand().AddCard(go.GetComponent<Card>());
                break;
            case PileType.deck:
                MainManager.instance.GetDeck().AddCard(go.GetComponent<Card>());
                break;
            case PileType.voidPile:
                break;
            default:
                break;
        }
        base.OnPlayed();
    }
}

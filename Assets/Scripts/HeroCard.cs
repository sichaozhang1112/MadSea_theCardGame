using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCard : FriendlyCard
{
    [SerializeField] int goldAcquire;
    [SerializeField] int attackAcquire;
    [SerializeField] int exileHand;
    [SerializeField] int exileField;
    [SerializeField] int discardHand;
    [SerializeField] int heal;
    [SerializeField] int draw;
    [SerializeField] int score;
    [SerializeField] bool oneUse = false;
    [SerializeField] bool shouldHaveHand = false;


    override public void OnPlayed()
    {
        if (shouldHaveHand)
        {
            int curHand = MainManager.instance.GetHand().GetCount();
            if (curHand < discardHand || curHand < exileHand){
                return;
            }
        }
        MainManager.instance.AddAttack(attackAcquire);
        MainManager.instance.AddGold(goldAcquire);
        MainManager.instance.LifeChange(heal);
        MainManager.instance.GetHand().DrawCards(draw);
        MainManager.instance.ScoreChange(score);
        if (oneUse)
        {
            MainManager.instance.GetVoidPile().AddCard(this);
        }
        else
        {
            MainManager.instance.GetPlayedCards().AddCard(this);
        }
        if (exileHand > 0 || exileField > 0) MainManager.instance.StartExile(exileField, exileHand);
        if (discardHand > 0) MainManager.instance.StartDiscard(discardHand); 
    }


}

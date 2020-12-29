using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCard : Card
{
    [SerializeField] int targetCharge;
    [SerializeField] int attack;
    [SerializeField] int score;
    [SerializeField] int goldAcquire;
    [SerializeField] int attackAcquire;
    [SerializeField] int exileField;
    [SerializeField] int exileHand;

    bool lastAttacked = false;

    int curCharge = 0;

    public override bool GetLastAttacked()
    {
        if (lastAttacked)
        {
            lastAttacked = false;
            return true;
        }
        return false;

    }

    public virtual void OnBeaten()
    {
        MainManager.instance.GetVoidPile().AddCard(this);
        MainManager.instance.ScoreChange(score);
        MainManager.instance.AddGold(goldAcquire);
        MainManager.instance.AddAttack(attackAcquire);
        if(exileField>0 || exileHand > 0)
        {
            MainManager.instance.StartExile(exileField, exileHand);
        }
    }

    public override void OnGetNormally()
    {
        OnBeaten();
    }

    public override void GetAddonUI(out CardAddonUIType type, out int value, out int maxvalue)
    {
        type = CardAddonUIType.charge;
        value = curCharge;
        maxvalue = targetCharge;
    }

    public bool Charge()
    {
        curCharge += 1;
        if (curCharge >= targetCharge)
        {
            curCharge = 0;
            ReleaseWrath();
            return true;
        }
        return false;
    }

    public override bool OnWrath()
    {
        return Charge();
    }

    public virtual void ReleaseWrath()
    {
        lastAttacked = true;
        MainManager.instance.LifeChange(-attack);
    }
}

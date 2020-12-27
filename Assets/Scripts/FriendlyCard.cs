using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyCard : Card
{
    [SerializeField] int targetCurse;
    int curCurse = 0;
    [SerializeField] GameObject cursedCard;
    //bool lastCursed;

    //public override bool GetLastCursed()
    //{
    //    if (lastCursed)
    //    {
    //        lastCursed = false;
    //        return true;
    //    }

    //    return false;
    //}

    public virtual void OnAcquire()
    {
        targetCurse = 0;
        curCurse = 0;
        MainManager.instance.GetDiscardPile().AddCard(this);
    }

    public override void OnGetNormally()
    {
        OnAcquire();
    }

    override public void OnPlayed()
    {
        MainManager.instance.GetPlayedCards().AddCard(this);
    }

    public override bool OnCurse()
    {
        if (targetCurse > 0)
        {
            curCurse += 1;
            if (curCurse >= targetCurse)
            {
                lastCursed = true;
                return true;
            }
        }

        return false;
    }

    public override GameObject GetCursedCard()
    {
        return cursedCard;
    }

    public override void GetAddonUI(out CardAddonUIType type, out int value, out int maxvalue)
    {
        if (targetCurse > 0)
        {
            type = CardAddonUIType.curse;
            value = curCurse;
            maxvalue = targetCurse;
        }
        else
        {
            base.GetAddonUI(out type, out value, out maxvalue);
        }
    }

}

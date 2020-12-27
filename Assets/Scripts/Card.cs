using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CostType
{
    gold,
    attack
}

public class Card : MonoBehaviour
{
    [SerializeField] string cardName;
    [SerializeField] CostType costType;
    [SerializeField] int cost;
    [SerializeField] string detail;
    [SerializeField] bool flagExile = true;
    protected bool lastCursed = false;



    public string getCardName() { return cardName; }
    public CostType getCostType() { return costType; }
    public int getCost() { return cost; }
    public string getDetail() { return detail; }
    public bool canExile() { return flagExile; }

    public virtual void GetAddonUI(out CardAddonUIType type, out int value, out int maxvalue)
    {
        type = CardAddonUIType.none;
        value = 0;
        maxvalue = 0;
    }


    public virtual void OnPlayed()
    {
        MainManager.instance.GetPlayedCards().AddCard(this);
    }

    public virtual void OnExile()
    {
        MainManager.instance.GetVoidPile().AddCard(this);
    }

    public virtual bool OnWrath() { return false; }

    public virtual bool OnCurse() { return false; }

    public virtual bool GetLastAttacked() { return false; }

    public virtual bool GetLastCursed()
    {
        if (lastCursed)
        {
            lastCursed = false;
            return true;
        }

        return false;
    }
    public void SetLastCursed() { lastCursed = true; }

    public virtual GameObject GetCursedCard() { Debug.LogWarning("Invalid call"); return null; }

    public virtual void OnGetNormally() { }

    public bool RequirementsCheck()
    {
        int curCost = MainManager.instance.GetResource(costType);
        if (curCost >= cost) return true; else return false;
    }
}

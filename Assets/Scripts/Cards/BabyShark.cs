using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyShark : HeroCard
{
    override public void OnPlayed()
    {
        int ran = Random.Range(0, 100);
        if (ran < 10)
        {
            MainManager.instance.LifeChange(-1);
        }
        else
        {
            MainManager.instance.AddAttack(3);
        }
    }
}

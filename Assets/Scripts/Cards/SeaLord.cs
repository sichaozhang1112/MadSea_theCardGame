using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaLord : EnemyCard
{
    public override void OnBeaten()
    {
        MainManager.instance.OnBeatSeaLord();
        base.OnBeaten();
    }
}

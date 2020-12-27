using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftingBox : HeroCard
{
    [SerializeField] List<GameObject> rewards;
    [SerializeField] List<int> props;

    int propSum;
    public override void OnAcquire()
    {
        MainManager.instance.GetVoidPile().AddCard(this);
        int ran = Random.Range(0, propSum);
        for (int i = 0; i < props.Count; i++)
        {
            ran -= props[i];
            if (ran <= 0)
            {
                GameObject go = GameObject.Instantiate(rewards[i], MainManager.instance.GetCardsInstance().transform);
                MainManager.instance.GetHand().AddCard(go.GetComponent<Card>());
                break;
            }
        }
    }

    private void Start()
    {
        propSum = 0;
        foreach (var item in props)
        {
            propSum += item;
        }
    }
}

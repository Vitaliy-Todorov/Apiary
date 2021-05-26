using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CountingBees
{
    protected List<GameObject> beesHive = new List<GameObject>();

    public CountingBees(List<GameObject> beesHive)
    {
        this.beesHive = beesHive;
    }

    public int CountingBee()
    {
        return beesHive.Count(bee => bee.GetComponent<Bee>()); ;
    }
}

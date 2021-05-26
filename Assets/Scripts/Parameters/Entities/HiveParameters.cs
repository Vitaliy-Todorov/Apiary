using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hive", menuName = "Apiary/Hive", order = 2)]
public class HiveParameters : SpawningDifferentObjectsParameters
{
    [SerializeField]
    public float maxHoney;
    [SerializeField]
    public float timeBeesGiveHoney;
    [SerializeField]
    public DeathBeesParameters deathBees;
}

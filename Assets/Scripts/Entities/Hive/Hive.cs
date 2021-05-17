using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    GeneratingObject generatingObject;
    [SerializeField]
    HiveParameters parameters;
    [SerializeField]
    HiveMenu hiveMenu;

    float сurrentHoneyStocks = 0;
    int сurrentBees = 0;

    void Start()
    {
        generatingObject = gameObject.AddComponent<GeneratingObject>();
        generatingObject.Init(parameters.timeNewBee, parameters.appearsAtTime, parameters.maxNumberBee, gameObject, parameters.bee);
        hiveMenu.MaxValue(parameters.maxHoney, parameters.maxNumberBee);
        hiveMenu.SetBees(0);
        hiveMenu.SetHoney(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        сurrentHoneyStocks += 10;
        сurrentBees += 1;

        hiveMenu.SetHoney(сurrentHoneyStocks);
        hiveMenu.SetBees(сurrentBees);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : SpawningDifferentObjects
{
    SpawningDifferentObjects spawningDifferentObjects;
    [SerializeField]
    HiveParameters parameters;
    [SerializeField]
    HiveMenu hiveMenu;

    float сurrentHoneyStocks = 0;
    int сurrentBees = 0;

    void Start()
    {
        //Создаём спавнер
        Init(parameters.appearsAtTime, parameters.maxNumberBee, gameObject, parameters.spawningObject);
        coroutine = CreateObject();
        StartCoroutine(coroutine);
        //Подключаем меню улья
        hiveMenu.MaxValue(parameters.maxHoney, parameters.maxNumberBee);
        hiveMenu.SetBees(0);
        hiveMenu.SetHoney(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bee dee = collision.gameObject.GetComponent<Bee>();
        //Если это не пчела, не взаимодействуем
        if (!dee)
            return;

        if (createObjects.Contains(collision.gameObject) && dee.FilledHoneyStocks())
            toHive(dee);

        сurrentBees += 1;

        hiveMenu.SetHoney(сurrentHoneyStocks);
        hiveMenu.SetBees(сurrentBees);
    }

    void toHive(Bee dee)
    {
        сurrentHoneyStocks += dee.GettHoney();
    }
}

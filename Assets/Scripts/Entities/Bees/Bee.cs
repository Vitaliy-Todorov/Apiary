using System.Collections;
using UnityEngine;

//HoneyConsumer нужен что бы цветок понимал, что у него берут мёд
public class Bee : FlyingInsect, IGeneratedObject
{
    [SerializeField]
    new public BeesParameters parameters;
    [SerializeField]
    float сurrentHoneyStocks = 0;
    IHoneyGiver _honeyGiver;

    public CollectGiveHoneyBee _stateHoneyGetter;

    //Позже избавиться от этого, используется в HoneyConsumer
    public float СurrentHoneyStocks { get => сurrentHoneyStocks; set => сurrentHoneyStocks = value; }

    new private void Start()
    {
        base.Start();
        //Идём к ближайшему свободному цветку
        _stateMovement.OnEnter<HoneyGoTo>();
        _stateHoneyGetter = gameObject.AddComponent<CollectGiveHoneyBee>();
        _stateHoneyGetter.Init(this);
    }

    public float GettHoney(float gettHoney)
    {
        if (сurrentHoneyStocks - gettHoney <= gettHoney)
        {
            float honey = сurrentHoneyStocks;
            сurrentHoneyStocks = 0;
            _stateMovement.OnEnter<HoneyGoTo>();
            return honey;
        }
        сurrentHoneyStocks -= gettHoney;
        return gettHoney;
    }

    public float GettHoney()
    {
        float honey = сurrentHoneyStocks;
        сurrentHoneyStocks = 0;
        _stateMovement.OnEnter<HoneyGoTo>();
        return honey;
    }

    public bool FilledHoneyStocks()
    {
        return (сurrentHoneyStocks >= parameters.maxHoneyStocks);
    }
}

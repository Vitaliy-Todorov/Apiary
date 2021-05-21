using System.Collections;
using UnityEngine;

//HoneyConsumer нужен что бы цветок понимал, что у него берут мёд
public class Bee : Motion, IGeneratedObject
{
    [SerializeField]
    public BeesParameters parameters;
    [SerializeField]
    float сurrentHoneyStocks = 0;
    //Задаётся при спавне, что бы связать префаб и экземпляр на сцене (ну или вообще, если нужно связать элемент списка и экземпляр на сцене)
    string _id;
    IHoneyGiver _honeyGiver;

    //Используется в классе HiveGoTo
    GameObject _hiveThisBee;

    public MovementBee _stateMovement;
    public CollectGiveHoneyBee _stateHoneyGetter;

    //Позже избавиться от этого, используется в HoneyConsumer
    public float СurrentHoneyStocks { get => сurrentHoneyStocks; set => сurrentHoneyStocks = value; }
    public string Id { get => _id; }

    public void Init(GameObject hiveThisBee, string id)
    {
        _hiveThisBee = hiveThisBee;
        _id = id;
    }

    private void Start()
    {
        _stateMovement = gameObject.AddComponent<MovementBee>();
        _stateMovement.Init();
        //Задаём точку куда будет возврашаться пчела после сбора мёда
        _stateMovement.OnEnter<GoTo>(_hiveThisBee.transform.position);
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

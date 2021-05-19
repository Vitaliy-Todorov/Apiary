using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    MovementBee _stateMovement;
    HoneyGetterBee _stateHoneyGetter;

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
        _stateHoneyGetter = new HoneyGetterBee(gameObject, _stateMovement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Если объект может давать мёд, то берём его
        _honeyGiver = collision.gameObject.GetComponent<IHoneyGiver>();

        if (_honeyGiver is IHoneyGiver && сurrentHoneyStocks < parameters.maxHoneyStocks)
        {
            _stateMovement.OnExit();
            _stateHoneyGetter.OnEnter(_honeyGiver);
        }
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
        if (сurrentHoneyStocks >= parameters.maxHoneyStocks)
            return true;
        else
            return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Motion, IHoneyGetter, IGeneratedObject
{
    [SerializeField]
    public BeesParameters parameters;
    [SerializeField]
    float сurrentHoneyStocks = 0;
    IHoneyGiver _honeyGiver;

    //Используется в классе HiveGoTo
    GameObject _hiveThisBee;

    MovementBee _stateMovement;
    HoneyGetterBee _stateHoneyGetter;

    public void Init(GameObject hiveThisBee)
    {
        _hiveThisBee = hiveThisBee;
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

    public IEnumerator HoneyGet(IHoneyGiver honeyGiver, float waitTime)
    {
        throw new System.NotImplementedException();
    }
}

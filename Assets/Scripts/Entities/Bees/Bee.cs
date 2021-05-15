using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Motion, IHoneyGetter
{
    [SerializeField]
    public BeesParameters parameters;
    [SerializeField]
    float сurrentHoneyStocks = 0;
    IHoneyGiver _honeyGiver;

    //Используется в классе HiveGoTo
    public GameObject _hiveThisBee;

    MovementBee _stateMovement;
    HoneyGetterBee _stateHoneyGetter;

    private void Start()
    {
        _stateMovement = gameObject.AddComponent<MovementBee>();
        _stateMovement.Init();
        _stateMovement.OnEnter<GoTo>(new Vector3(5, 0, 5));
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

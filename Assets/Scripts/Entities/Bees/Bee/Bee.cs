using UnityEngine;

//HoneyConsumer нужен что бы цветок понимал, что у него берут мёд
public class Bee : HiveDweller, IGeneratedObject
{
    new public BeesParameters parameters;
    [SerializeField]
    float сurrentHoneyStocks = 0;

    public CollectGiveHoneyBee _stateHoneyGetter;

    //Позже избавиться от этого, используется в HoneyConsumer
    public float СurrentHoneyStocks { get => сurrentHoneyStocks; set => сurrentHoneyStocks = value; }

    public override void WorkingGoTo()
    {
        _stateHoneyGetter.OnEnter();
        _stateMovement.OnEnterGoToHoney();
    }

    public override void GoTo()
    {
        _stateHoneyGetter.OnExit();
        _stateMovement.OnEnterGoTo();
    }

    void Start()
    {
        _stateMovement = gameObject.AddComponent<MovementInsect>();
        _stateMovement.Init((GoToParameters)parameters);
        _stateMovement.Init(parameters);

        _stateHoneyGetter = gameObject.AddComponent<CollectGiveHoneyBee>();
        _stateHoneyGetter.Init(this);
        _stateMovement.OnEnterGoToHoney();
    }

    public float GettHoney(float gettHoney)
    {
        if (сurrentHoneyStocks - gettHoney <= gettHoney)
        {
            float honey = сurrentHoneyStocks;
            сurrentHoneyStocks = 0;
            _stateMovement.OnEnterGoToHoney();
            return honey;
        }
        сurrentHoneyStocks -= gettHoney;
        return gettHoney;
    }

    public float GettHoney()
    {
        float honey = сurrentHoneyStocks;
        сurrentHoneyStocks = 0;
        _stateMovement.OnEnterGoToHoney();
        return honey;
    }

    public bool FilledHoneyStocks()
    {
        return (сurrentHoneyStocks >= parameters.maxHoneyStocks);
    }
}

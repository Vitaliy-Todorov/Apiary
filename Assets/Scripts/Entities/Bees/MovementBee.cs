using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Для перемещения. Перед использованием нужно активировать через OnEnter(float speed, string goTo)
/// </summary>
public class MovementBee : MonoBehaviour, IState
{
    MovementBeeState currentState;
    HoneyGoTo _honeyGoTo;
    GoTo _goTo;
    BeesParameters _parameters;

    private void Start()
    {
        _parameters = gameObject.GetComponent<Bee>().parameters;
        _honeyGoTo = new HoneyGoTo(gameObject);
        _goTo = new GoTo(gameObject);
        currentState = _honeyGoTo;
    }

    public void OnEnter()
    {
        throw new NotImplementedException();
    }

    public void OnEnter<T>()
    {
        enabled = true;
        if (typeof(T) == typeof(HoneyGoTo))
            currentState = _honeyGoTo;
        else if (typeof(T) == typeof(GoTo))
            currentState = _goTo;
        else
            throw new ArgumentException("There is no such state: " + typeof(T));
    }

    public void OnEnter<T>(object signal)
    {
        if (typeof(T) == typeof(GoTo))
        {
            currentState = _goTo;
            _goTo._weMove = (Vector3)signal;
        }
    }

    public void OnEnter(object signal)
    {
        throw new NotImplementedException();
    }

    public void OnExit()
    {
        enabled = false;
    }

    private void FixedUpdate()
    {
        Motion.Move(transform, currentState.GoTu, _parameters.speed);
    }
}



public abstract class MovementBeeState
{
    public virtual Vector3 GoTu { get; }
}



public class HoneyGoTo : MovementBeeState
{
    GameObject _bee;

    public HoneyGoTo(GameObject weMove)
    {
        _bee = weMove;
    }

    public override Vector3 GoTu
    {
        get => MinDistanceToFlowers(Flowers.freeFlowers);
    }

    Vector3 MinDistanceToFlowers(List<GameObject> distanceTo)
    {
        Vector3 distanceToFlower = new Vector3();
        Vector3 minDistanceToFlower = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        foreach (GameObject flower in distanceTo)
        {
            //Проверяем свободен ли цветок
            if (flower.GetComponent<IHoneyGiver>().CanCollectHoney())
            {
                //Ищем самый близкий цветок
                distanceToFlower = flower.transform.position - _bee.transform.position;
                if (distanceToFlower.magnitude < minDistanceToFlower.magnitude)
                    minDistanceToFlower = distanceToFlower;
            }
        }
        _ = minDistanceToFlower;
        return minDistanceToFlower;
    }
}


/// <summary>
/// Если при создании не назначить точку возврата движется в (0, 0, 0)
/// </summary>
public class GoTo : MovementBeeState
{
    GameObject _bee;
    public Vector3 _weMove;

    public GoTo(GameObject bee, Vector3 weMove)
    {
        _bee = bee;
        _weMove = weMove;
    }
    public GoTo(GameObject bee)
    {
        _bee = bee;
        _weMove = new Vector3();
    }

public override Vector3 GoTu
    {
        get => _weMove - _bee.transform.position;
    }
}

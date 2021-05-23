using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Для перемещения. Перед использованием нужно активировать через OnEnter(float speed, string goTo)
/// </summary>
public class MovementBees : MonoBehaviour, IState
{
    MovementState currentState;
    GoToHoney _honeyGoTo;
    GoTo _goTo;
    GoToRandom _goToRandom;
    FlyingInsect _flyingInsect;
    FlyingInsectParameters _parameters;

    IEnumerator generatingRandomPoint;

    public void Init(FlyingInsect flyingInsect)
    {
        _flyingInsect = flyingInsect;
        _parameters = flyingInsect.parameters;
        _honeyGoTo = new GoToHoney(gameObject);
        _goTo = new GoTo(gameObject);
        _goToRandom = new GoToRandom(gameObject);
    }

    public void OnEnter() => enabled = true;

    public void OnExit() => enabled = false;

    public void OnEnter<T>()
    {
        enabled = true;
        if (typeof(T) == typeof(GoToHoney))
            currentState = _honeyGoTo;
        else if (typeof(T) == typeof(GoTo))
            currentState = _goTo;
        else if (typeof(T) == typeof(GoTo))
            currentState = _goTo;
        else
            throw new ArgumentException("There is no such state: " + typeof(T));
    }

    //Если нужно поминять точку в которую передвигается объект 
    public void OnEnter<T>(object vector3)
    {
        enabled = true;
        if (typeof(T) == typeof(GoTo))
        {
            currentState = _goTo;
            _goTo._weMove = (Vector3)vector3;
        }
        else if (typeof(T) == typeof(GoToRandom))
        {
            currentState = _goToRandom;
            _goToRandom._trafficArea = (Vector3)vector3;
        }
        else
            throw new ArgumentException("This state does not accept such a parameter");
    }

    private void Start()
    {
        generatingRandomPoint = GeneratingRandomPoint();
        StartCoroutine(generatingRandomPoint);
    }

    protected IEnumerator GeneratingRandomPoint()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
        }
    }

    private void FixedUpdate() 
    {
        Motion.Move(transform, currentState.GoTu, _parameters.speed); 
    } 
}



public abstract class MovementState
{
    public virtual Vector3 GoTu { get => new Vector3(); }
}


public class GoToHoney : MovementState
{
    GameObject _bee;

    public GoToHoney(GameObject weMove) =>_bee = weMove;

    public override Vector3 GoTu
    {
        get => MinDistanceToFlowers(Flowers.freeFlowers);
    }

    Vector3 MinDistanceToFlowers(List<GameObject> distanceTo)
    {
        Vector3 distanceToFlower = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 minDistanceToFlower = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        foreach (GameObject flower in distanceTo)
        {
            if (flower != null)
                distanceToFlower = flower.transform.position - _bee.transform.position;
            if (distanceToFlower.magnitude < minDistanceToFlower.magnitude)
                minDistanceToFlower = distanceToFlower;
        }

        return minDistanceToFlower;
    }
}


/// <summary>
/// Объект будет возвращаться в указанную точку. Если при создании не назначить точку возврата движется в (0, 0, 0)
/// </summary>
public class GoTo : MovementState
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

public class GoToRandom : MovementState
{
    GameObject _bee;
    public Vector3 _trafficArea;
    public Vector3 _centerOfTrafficArea;

    public GoToRandom(GameObject bee)
    {
        _bee = bee;
    }

    public GoToRandom(GameObject bee, Vector3 trafficArea)
    {
        _bee = bee;
        _trafficArea = trafficArea;
    }

    public override Vector3 GoTu
    {
        get => SpawnRandomPoint();
    }

    Vector3 SpawnRandomPoint()
    {
        Vector3 offset = new Vector3();
        offset.y = _trafficArea.y;
        //Получаем размеры Mesh (думаю это можно назвать физическим пространством объекта). Это нужно, что бы получить ограничения места респаума
        offset.x = _trafficArea.x * UnityEngine.Random.Range(-.5f, .5f);
        offset.z = _trafficArea.z * UnityEngine.Random.Range(-.5f, .5f);

        //Debug.Log(offset);
        //Debug.Log(_centerOfTrafficArea.transform.position);
        return offset + _centerOfTrafficArea;
    }
}

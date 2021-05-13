using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Для перемещения
/// </summary>
public class MovementStateBee : MonoBehaviour, IState
{
    float _speed;
    Vector3 _goToVector3;
    string _goTo;
    public bool B;

    public void OnEnter(float speed, string goTo)
    {
        _speed = speed;
        B = false;
        Signal(goTo);
    }
    public void OnEnter()
    {
        //enabled = true;

        B = false;
    }

    public void OnEnter(string goTo)
    {
        B = false;

        //enabled = true;
        Signal(goTo);
    }

    public void OnExit()
    {
        //enabled = false;
        B = true;
    }

    private void FixedUpdate()
    {
        if (B)
            return;
        if (_goTo == "Honey")
            _goToVector3 = MinDistanceToFlowers(Flowers.allFlowers);
        else if (_goTo == "Hive")
            _goToVector3 = gameObject.GetComponent<Bee>()._hiveThisBee.transform.position;

        Motion.Move(transform, _goToVector3, _speed);
    }

    /// <summary>
    ///  goTo - возможные значения: "Hive" и "Honey" искать мёд
    /// </summary>
    public void Signal(float speed, string goTo)
    {
        _speed = speed;
        if (goTo == "Honey" || goTo == "Honey")
            _goTo = goTo;
        else
            throw new ArgumentException("There is no such value: " + goTo);
    }

    /// <summary>
    ///  goTo - возможные значения: "Hive" и "Honey" искать мёд
    /// </summary>
    public void Signal(string goTo)
    {
        if (goTo == "Honey" || goTo == "Hive")
            _goTo = goTo;
        else
            throw new ArgumentException("There is no such value: " + goTo);

    }

    public void Signal(float speed, Vector3 goTo)
    {
        _goToVector3 = goTo;

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
                distanceToFlower = flower.transform.position - transform.position;
                if (distanceToFlower.magnitude < minDistanceToFlower.magnitude)
                    minDistanceToFlower = distanceToFlower;
            }
        }

        _ = minDistanceToFlower;
        return minDistanceToFlower;
    }
}

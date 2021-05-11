using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MotionBee : Motion, IHoneyGetter
{
    [SerializeField]
    BeesParameters parameters;

    float сurrentHoneyStocks = 0;

    List<GameObject> HoneyGivers;

    private IEnumerator coroutine;

    delegate void Movement();

    private void Start()
    {
        HoneyGivers = new List<GameObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Если объект может давать мёд, то берём его
        IHoneyGiver honeyGiver = collision.gameObject.GetComponent<IHoneyGiver>();
        if (honeyGiver is IHoneyGiver)
        {
            coroutine = HoneyGet(honeyGiver, parameters.getHoneyTime);
            StartCoroutine(coroutine);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Здесь нужно написать отключение поглощения мёда
    }

    private void FixedUpdate()
    {
        Move(MinDistanceToFlowers(Flowers.allFlowers), parameters.speed);
    }

    public IEnumerator HoneyGet(IHoneyGiver honeyGiver, float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            //Проверяем может ли пчела ещё взять мёд и наличие объекта у которого мы хотим взять мёд
            if (сurrentHoneyStocks < parameters.maxHoneyStocks && !honeyGiver.Equals(null))
            {
                сurrentHoneyStocks +=
                    honeyGiver.HoneyGive(gameObject, parameters.getHoney);
            } 
        }
    }

    Vector3 MinDistanceToFlowers(List<GameObject> distanceTo)
    {
        Vector3 distanceToFlower = new Vector3();
        Vector3 minDistanceToFlower = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        foreach (GameObject flower in distanceTo)
        {
            //Проверяем свободен ли цветок
            if (flower.GetComponent<Flowers>().canCollectHoney)
            {
                //Ищем самый близкий цветок
                distanceToFlower = flower.transform.position - transform.position;
                if (distanceToFlower.magnitude < minDistanceToFlower.magnitude)
                    minDistanceToFlower = distanceToFlower;
            }
        }

        return minDistanceToFlower;
    }
}

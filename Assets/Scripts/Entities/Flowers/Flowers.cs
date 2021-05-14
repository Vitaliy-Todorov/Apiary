using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flowers : MonoBehaviour, IHoneyGiver
{
    public static List<GameObject> allFlowers = new List<GameObject>();

    List<GameObject> honeyGetters = new List<GameObject>();

    [SerializeField]
    FlowerParameters parameters;
    [SerializeField]
    float сurrentHoneyStocks = 10;

    bool canCollectHoney;

    IEnumerator coroutine;

    public bool CanCollectHoney () {return canCollectHoney;}

    private void Start()
    {
        canCollectHoney = true;
        allFlowers.Add(gameObject);
        сurrentHoneyStocks = parameters.maxHoneyStocks;

        coroutine = HoneyRecovery(parameters.honeyRecoveryTime);
        StartCoroutine(coroutine);
    }

    private void OnDestroy()
    {
        allFlowers.Remove(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Проверяем наличие свободных мест для тех кто хочет потреблять мёд
        if (collision.gameObject.GetComponent<IHoneyGetter>() is IHoneyGetter &&
            honeyGetters.Count < parameters.simultaneousUseByBees)
            honeyGetters.Add(collision.gameObject);

        //Если все места заняты canCollectHoney может сообщить, что к цветку идти ненадо
        if (honeyGetters.Count == parameters.simultaneousUseByBees)
            canCollectHoney = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (honeyGetters.Contains(collision.gameObject))
        {
            canCollectHoney = true;
            honeyGetters.Remove(collision.gameObject);
        }
    }

    public IEnumerator HoneyRecovery(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if(сurrentHoneyStocks < parameters.maxHoneyStocks)
                сurrentHoneyStocks += parameters.honeyRecovery;
        }
    }

    public float HoneyGive(GameObject whosAsking, float honey)
    {
        Debug.Log(whosAsking + ": " +"HoneyGive: " + !(honeyGetters.Contains(whosAsking)) + " canCollectHoney: " + !canCollectHoney + " = " + !(!(honeyGetters.Contains(whosAsking)) ^ !canCollectHoney));
        if (!(!(honeyGetters.Contains(whosAsking)) ^ !canCollectHoney))
            throw new ArgumentException("This object can't take honey. The seats are occupied or it doesn't have an IHoneyConsumer");

        if (сurrentHoneyStocks - honey >= 0)
        {
            сurrentHoneyStocks = сurrentHoneyStocks - honey;
            return honey;
        }
        else
        {
            Destroy(transform.root.gameObject);
            return сurrentHoneyStocks;
        }
    }
}

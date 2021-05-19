using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flowers : MonoBehaviour, IHoneyGiver, IGeneratedObject, IAddCamera
{
    public static List<GameObject> freeFlowers = new List<GameObject>();

    List<GameObject> honeyGetters = new List<GameObject>();

    [SerializeField]
    FlowerParameters parameters;
    [SerializeField]
    float сurrentHoneyStocks = 10;
    [SerializeField]
    GameObject progressBarGmObj;
    //Задаётся при спавне, что бы связать префаб и экземпляр на сцене (ну или вообще, если нужно связать элемент списка и экземпляр на сцене)
    string _id;
    IProgressBar progressBar;

    bool canCollectHoney;
    public bool isDestroyed;

    GameObject _generatingObject;

    IEnumerator coroutine;

    public string Id { get => _id; }

    public bool CanCollectHoney () {return canCollectHoney; }

    public void Init(GameObject generatingObject, string id)
    {
        _generatingObject = generatingObject;
        _id = id;
    }

    public bool IsDestroyed() { return isDestroyed;  }

    private void Start()
    {
        canCollectHoney = true;
        freeFlowers.Add(gameObject);
        сurrentHoneyStocks = parameters.maxHoneyStocks;

        progressBar = progressBarGmObj.GetComponent<IProgressBar>();
        progressBar.MaxValue(parameters.maxHoneyStocks);

        coroutine = HoneyRecovery(parameters.honeyRecoveryTime);
        StartCoroutine(coroutine);
    }

    private void OnDestroy()
    {
        freeFlowers.Remove(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Проверяем наличие свободных мест для тех кто хочет потреблять мёд
        if (collision.gameObject.GetComponent<HoneyConsumer>() is HoneyConsumer &&
            honeyGetters.Count < parameters.simultaneousUseByBees)
            honeyGetters.Add(collision.gameObject);

        //Если все места заняты canCollectHoney может сообщить, что к цветку идти ненадо
        if (honeyGetters.Count == parameters.simultaneousUseByBees)
        {
            canCollectHoney = false;
            freeFlowers.Remove(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (honeyGetters.Contains(collision.gameObject))
        {
            canCollectHoney = true;
            honeyGetters.Remove(collision.gameObject);
            freeFlowers.Add(gameObject);
        }
    }

    public IEnumerator HoneyRecovery(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if(сurrentHoneyStocks < parameters.maxHoneyStocks)
                сurrentHoneyStocks += parameters.honeyRecovery;
            progressBar.SetProgress(сurrentHoneyStocks);
        }
    }

    public float HoneyGive(GameObject whosAsking, float honey)
    {
        if (!(!(honeyGetters.Contains(whosAsking)) ^ !canCollectHoney))
            throw new ArgumentException("This object can't take honey. The seats are occupied or it doesn't have an IHoneyConsumer");

        if (сurrentHoneyStocks - honey >= 0)
        {
            сurrentHoneyStocks = сurrentHoneyStocks - honey;
            progressBar.SetProgress(сurrentHoneyStocks);
            return honey;
        }
        else
        {
            Destroy(transform.root.gameObject);
            isDestroyed = true;
            //DestroyImmediate(transform.root.gameObject);
            return сurrentHoneyStocks;
        }
    }

    public void AddCamera(Transform cam)
    {
        progressBarGmObj.GetComponent<Billdoard>().AddCamera(cam);
    }
}

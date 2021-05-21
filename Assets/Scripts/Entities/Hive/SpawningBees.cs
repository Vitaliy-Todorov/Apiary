using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBees : SpawningDifferentObjects, IState
{
    HiveParameters _parameters;

    protected IEnumerator createObject;
    protected IEnumerator beeInHives;

    public void Init(HiveParameters parameters)
    {
        _parameters = parameters;
    }

    public void OnEnter()
    {
        enabled = true;
    }

    public void OnExit()
    {
        enabled = false;
    }

    void Start()
    {
        //Создаём спавнер
        Init(_parameters, gameObject);
        createObject = CreateObject();
        StartCoroutine(createObject);
    }

    protected override Vector3 SpawnPoint()
    {
        return gameObject.transform.position;
    }
}

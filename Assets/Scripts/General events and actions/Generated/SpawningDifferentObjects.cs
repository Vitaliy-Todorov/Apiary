using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SpawningDifferentObjects : MonoBehaviour, IState
{
    [SerializeField]
    GameObject spawnerLocation;
    [SerializeField]
    Transform cameraForBar;
    [SerializeField]
    SpawningDifferentObjectsParameters baseParameters;
    List<ObjectAndFloat> spawningObject = new List<ObjectAndFloat>();

    protected List<GameObject> createObjects = new List<GameObject>();

    IEnumerator coroutineCreateObject;

    public List<GameObject> CreateObjects { get => createObjects; }

    public void Init(SpawningDifferentObjectsParameters parameters, GameObject spawnerLocation)
    {
        this.spawnerLocation = spawnerLocation;
        this.baseParameters = parameters;
        //Сортируем по возрастанию вероятности
        spawningObject = parameters.spawningObject.OrderBy(spObj => spObj.probs).ToList();
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
        spawningObject = baseParameters.spawningObject.OrderBy(spObj => spObj.probs).ToList();

        coroutineCreateObject = CreateObject();
        StartCoroutine(coroutineCreateObject);
    }

    protected IEnumerator CreateObject()
    {
        while (true)
        {
            DeleteDestroyed();

            for (int i = 0; i < baseParameters.appearsAtTime; i++)
                //Проверяем нужно ли ещё создавать пчёл и инициализирован ли список spawningObject
                if (createObjects.Count < baseParameters.maxNumberObject && spawningObject.Count != 0)
                {
                    //Выбираем из списка spawningObject какой объект создать с указанной в spawningObject вероятностью
                    int prefabIndex = ChooseIndexObject(spawningObject);
                    GameObject generatedObject = Instantiate(spawningObject[prefabIndex].spawnObject, SpawnPoint(), Quaternion.identity);
                    //Отправляем созданному объекту ссылку на создателя и prefab по которому был создан объект
                    if (generatedObject.GetComponent<IGeneratedObject>() != null)
                        generatedObject.GetComponent<IGeneratedObject>().Init(gameObject, spawningObject[prefabIndex].id);
                    //Добавляем в список созданных и существующих объектов
                    createObjects.Add(generatedObject);
                    //Переименовываем, чтобы различать объекты
                    generatedObject.name = generatedObject.name + createObjects.Count;
                    //Если есть Bar, или что-то ещё для чего нужна камера, отправляем на неё ссылку
                    if (generatedObject.GetComponent<IAddCamera>() != null)
                        generatedObject.GetComponent<IAddCamera>().AddCamera(cameraForBar);
                    //yield return new WaitForSeconds(.01f);
                }
                else
                    OnExit();

            yield return new WaitForSeconds(baseParameters.time);
        }
    }

    /// <summary>
    /// Получаем индекс объектаиз списка с определённой вероятностью
    /// </summary>
    int ChooseIndexObject(List<ObjectAndFloat> objectsAndProbs)
    {
        float total = 0;

        foreach (ObjectAndFloat elem in objectsAndProbs)
            total += elem.probs;

        float randomPoint = UnityEngine.Random.value * total;

        for (int i = 0; i < objectsAndProbs.Count; i++)
        {
            if (randomPoint < objectsAndProbs[i].probs)
                return i;
            else
                randomPoint -= objectsAndProbs[i].probs;
        }

        throw new ArgumentException("Ничего не выпало, вероятность задана неправильно");
    }

    ///Удаляем уничтоженные объекты из списка
    void DeleteDestroyed()
    {
        foreach (GameObject createObject in createObjects.ToList())
            if (createObject == null)
                createObjects.Remove(createObject);
    }

     protected virtual Vector3 SpawnPoint()
    {
        //Получаем размеры Mesh (думаю это можно назвать физическим пространством объекта). Это нужно, что бы получить ограничения места респаума
        Vector3 sizeLocation = spawnerLocation.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        float x = (sizeLocation.x - 2) * UnityEngine.Random.Range(-.5f, .5f);
        float z = (sizeLocation.z - 2) * UnityEngine.Random.Range(-.5f, .5f);

        return (new Vector3(x, 0.5f, z)) + spawnerLocation.transform.position;
    }
}

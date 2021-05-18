using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SpawningDifferentObjects : MonoBehaviour
{
    [SerializeField]
    public int appearsAtTime = 1;
    [SerializeField]
    public int maxNumberObject = 4;
    [SerializeField]
    GameObject spawnerLocation;
    [Header("Список типа ObjectAndFloat, с объектом и вероятностью")]
    [SerializeField]
    List<ObjectAndFloat> spawningObject = new List<ObjectAndFloat>();
    [SerializeField]
    Transform cameraForBar;

    protected List<GameObject> createObjects = new List<GameObject>();

    protected IEnumerator coroutine;

    public List<GameObject> CreateObjects { get => createObjects; }

    public void Init(int appearsAtTime, int maxNumberObject, GameObject spawnerLocation, List<ObjectAndFloat> spawningObject)
    {
        this.appearsAtTime = appearsAtTime;
        this.maxNumberObject = maxNumberObject;
        this.spawnerLocation = spawnerLocation;
        //Сортируем по возрастанию вероятности
        this.spawningObject = spawningObject.OrderBy(spObj => spObj.probs).ToList();
    }

    void Start()
    {
        coroutine = CreateObject();
        StartCoroutine(coroutine);
    }

    protected IEnumerator CreateObject()
    {
        while (true)
        {
            DeleteDestroyed();
            //Проверяем нужно ли ещё создавать пчёл и инициализирован ли список spawningObject
            if (createObjects.Count < maxNumberObject && spawningObject.Count != 0)
            {
                GameObject generatedObject = Instantiate(ChooseObject(spawningObject), SpawnPoint(), Quaternion.identity);
                //Отправляем созданному объекту ссылку на создателя
                if (generatedObject.GetComponent<IGeneratedObject>() != null)
                    generatedObject.GetComponent<IGeneratedObject>().Init(gameObject);
                //Добавляем в список созданных и существующих объектов
                createObjects.Add(generatedObject);
                //Переименовываем, чтобы различать объекты
                generatedObject.name = generatedObject.name + createObjects.Count;

                if (generatedObject.GetComponent<IAddCamera>() != null)
                    generatedObject.GetComponent<IAddCamera>().AddCamera(cameraForBar);
            }

            yield return new WaitForSeconds(appearsAtTime);
        }
    }

    /// <summary>
    /// Получаем объект с определённой вероятностью
    /// </summary>
    GameObject ChooseObject(List<ObjectAndFloat> objectsAndProbs)
    {

        float total = 0;

        foreach (ObjectAndFloat elem in objectsAndProbs)
            total += elem.probs;

        float randomPoint = UnityEngine.Random.value * total;

        for (int i = 0; i < objectsAndProbs.Count; i++)
        {
            if (randomPoint < objectsAndProbs[i].probs)
                return objectsAndProbs[i].spawnObject;
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

    Vector3 SpawnPoint()
    {
        //Получаем размеры Mesh (думаю это можно назвать физическим пространством объекта). Это нужно, что бы получить ограничения места респаума
        Vector3 sizeLocation = spawnerLocation.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        float x = (sizeLocation.x - 2) * UnityEngine.Random.Range(-.5f, .5f);
        float z = (sizeLocation.z - 2) * UnityEngine.Random.Range(-.5f, .5f);

        return (new Vector3(x, 0.5f, z)) + spawnerLocation.transform.position;
    }
}

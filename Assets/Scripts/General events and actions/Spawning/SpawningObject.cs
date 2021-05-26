using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawningObject : MonoBehaviour
{
    [SerializeField]
    SpawningObjectParameters parameters;
    [SerializeField]
    public GameObject spawnerLocation;
    [SerializeField]
    public Transform cameraForBar;

    List<GameObject> createObjects;

    public void Init(SpawningObjectParameters parameters)
    {
        this.parameters = parameters;
    }

    void Start()
    {
        createObjects = new List<GameObject>();
        SpawnerDelay();
    }

    void SpawnerDelay()
    {
        DeleteDestroyed();
        for(int i = 0; i < parameters.appearsAtTime; i++)
            CreateObject();
        Invoke("SpawnerDelay", parameters.time);
    }

    protected void CreateObject()
    {
        if (createObjects.Count < parameters.maxNumberObject)
        {
            Vector3 spawnPoint;
            spawnPoint = SpawnPoint();
            //Проверяем наличие соседей (соприкосновений с ними)
            if (Physics.OverlapBox(
                    spawnPoint, 
                    new Vector3(parameters.distance, parameters.distance, parameters.distance), 
                    Quaternion.identity, 
                    LayerMask.GetMask(parameters.neighborLayer)
                    ).Length == 0)
            {
                GameObject generatedObject = Instantiate(parameters.createObjectInStance, spawnPoint, Quaternion.identity);
                //Отправляем созданному объекту ссылку на создателя
                generatedObject.GetComponent<IGeneratedObject>().Init(gameObject, parameters.createObjectInStance.name);
                //Добавляем в список созданных и существующих объектов
                createObjects.Add(generatedObject);
                //Переименовываем, чтобы различать объекты
                generatedObject.name = generatedObject.name + createObjects.Count;

                //Если нужжно передаём положение камеры созданному объекту
                if (generatedObject.GetComponent<IAddCamera>() != null)
                    generatedObject.GetComponent<IAddCamera>().AddCamera(cameraForBar);
            }
        }
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
        float x = (parameters.spawnerLocationX) * Random.Range(-.5f, .5f);
        float z = (parameters.spawnerLocationZ) * Random.Range(-.5f, .5f);

        return (new Vector3(x, 0.5f, z)) + gameObject.transform.position;
    }
}

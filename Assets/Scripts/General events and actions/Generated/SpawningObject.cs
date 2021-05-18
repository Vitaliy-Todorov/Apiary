using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawningObject : MonoBehaviour
{
    [SerializeField]
    public float time = 1;
    [SerializeField]
    public int appearsAtTime = 1;
    [SerializeField]
    public int maxNumberObject = 4;
    [SerializeField]
    GameObject _spawnerLocation;
    [SerializeField]
    GameObject _createObjectInStance;
    [SerializeField]
    Transform cameraForBar;

    List<GameObject> createObjects;

    public void Init(float time, int appearsAtTime, int maxNumberObject, GameObject spawnerLocation, GameObject createObjectInStance)
    {
        this.time = time;
        this.appearsAtTime = appearsAtTime;
        this.maxNumberObject = maxNumberObject;
        _spawnerLocation = spawnerLocation;
        _createObjectInStance = createObjectInStance;
    }

    void Start()
    {
        createObjects = new List<GameObject>();
        SpawnerDelay();
    }

    void SpawnerDelay()
    {
        DeleteDestroyed();
        for(int i = 0; i < appearsAtTime; i++)
            CreateObject();
        Invoke("SpawnerDelay", time);
    }

    protected void CreateObject()
    {
        if (createObjects.Count < maxNumberObject)
        {
            GameObject generatedObject = Instantiate(_createObjectInStance, SpawnPoint(), Quaternion.identity);
            //Отправляем созданному объекту ссылку на создателя
            generatedObject.GetComponent<IGeneratedObject>().Init(gameObject);
            //Добавляем в список созданных и существующих объектов
            createObjects.Add(generatedObject);
            //Переименовываем, чтобы различать объекты
            generatedObject.name = generatedObject.name + createObjects.Count;

            if(generatedObject.GetComponent<IAddCamera>() != null)
                generatedObject.GetComponent<IAddCamera>().AddCamera(cameraForBar);
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
        Vector3 sizeLocation = _spawnerLocation.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        float x = (sizeLocation.x - 2) * Random.Range(-.5f, .5f);
        float z = (sizeLocation.z - 2) * Random.Range(-.5f, .5f);

        return (new Vector3(x, 0.5f, z)) + _spawnerLocation.transform.position;
    }
}

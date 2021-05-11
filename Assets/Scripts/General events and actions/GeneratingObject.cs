using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeneratingObject : MonoBehaviour
{
    [SerializeField]
    public float time = 1;
    [SerializeField]
    public int maxNumberObject = 4;
    [SerializeField]
    GameObject _spawnerLocation;
    [SerializeField]
    GameObject createObjectInStance;

    List<GameObject> createObjects;

    public GeneratingObject(float time, int maxNumberObject, GameObject spawnerLocation)
    {
        this.time = time;
        this.maxNumberObject = maxNumberObject;
        _spawnerLocation = spawnerLocation;
    }

    void Start()
    {
        createObjects = new List<GameObject>();
        SpawnerDelay();
    }

    void SpawnerDelay()
    {
        CreateObject();
        Invoke("SpawnerDelay", time);
    }

    void CreateObject()
    {
        //Получаем размеры Mesh (думаю это можно назвать физическим пространством объекта). Это нужно, что бы получить ограничения места респаума
        Vector3 sizeLocation = _spawnerLocation.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        float x = (sizeLocation.x - 2) * Random.Range(-.5f, .5f);
        float z = (sizeLocation.z - 2) * Random.Range(-.5f, .5f);
        Vector3 position = new Vector3(x, 0.5f, z);
        Quaternion ganRotation = new Quaternion();

        //Удаляем уничтоженные объекты из списка
        foreach (GameObject createObject in createObjects.ToList())
            if (createObject == null)
                createObjects.Remove(createObject);

        if(createObjects.Count < maxNumberObject)
            createObjects.Add(Instantiate(createObjectInStance, position, ganRotation));
    }
}

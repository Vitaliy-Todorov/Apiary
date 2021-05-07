using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
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

    public Spawner(float time, int maxNumberObject, GameObject spawnerLocation)
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
        float x = (sizeLocation.x - 1) * Random.Range(-.5f, .5f);
        float z = (sizeLocation.z - 1) * Random.Range(-.5f, .5f);
        Vector3 position = new Vector3(x, 0.5f, z);
        Quaternion ganRotation = new Quaternion();

        //Считаем количиство уничтоженных объектов, для того, что бы узнать нужноли нам создовать новый
        int destroyedObjects = 0;
        foreach (GameObject createObject in createObjects)
            if (createObject == null)
                destroyedObjects = +1;

        if(createObjects.Count - destroyedObjects < maxNumberObject)
            createObjects.Add(Instantiate(createObjectInStance, position, ganRotation));
    }
}

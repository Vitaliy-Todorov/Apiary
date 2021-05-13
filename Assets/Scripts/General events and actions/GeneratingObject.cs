using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeneratingObject : MonoBehaviour
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
        DeleteDestroyed();
        for(int i = 0; i < appearsAtTime; i++)
            CreateObject();
        Invoke("SpawnerDelay", time);
    }

    void CreateObject()
    {
        Quaternion ganRotation = new Quaternion();

        if(createObjects.Count < maxNumberObject)
        {
            createObjects.Add(Instantiate(createObjectInStance, RandomVector(), ganRotation));
            createObjects[createObjects.Count - 1].name = createObjects[createObjects.Count - 1].name + createObjects.Count;
        }
    }

    void DeleteDestroyed()
    {
        //Удаляем уничтоженные объекты из списка
        foreach (GameObject createObject in createObjects.ToList())
            if (createObject == null)
                createObjects.Remove(createObject);
    }

    Vector3 RandomVector()
    {
        //Получаем размеры Mesh (думаю это можно назвать физическим пространством объекта). Это нужно, что бы получить ограничения места респаума
        Vector3 sizeLocation = _spawnerLocation.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        float x = (sizeLocation.x - 2) * Random.Range(-.5f, .5f);
        float z = (sizeLocation.z - 2) * Random.Range(-.5f, .5f);

        return new Vector3(x, 0.5f, z);
    }
}

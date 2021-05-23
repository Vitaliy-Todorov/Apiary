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
            //��������� ������� ������� (��������������� � ����)
            if (Physics.OverlapBox(
                    spawnPoint, 
                    new Vector3(parameters.distance, parameters.distance, parameters.distance), 
                    Quaternion.identity, 
                    LayerMask.GetMask(parameters.neighborLayer)
                    ).Length == 0)
            {
                GameObject generatedObject = Instantiate(parameters.createObjectInStance, spawnPoint, Quaternion.identity);
                //���������� ���������� ������� ������ �� ���������
                generatedObject.GetComponent<IGeneratedObject>().Init(gameObject, parameters.createObjectInStance.name);
                //��������� � ������ ��������� � ������������ ��������
                createObjects.Add(generatedObject);
                //���������������, ����� ��������� �������
                generatedObject.name = generatedObject.name + createObjects.Count;

                //���� ������ ������� ��������� ������ ���������� �������
                if (generatedObject.GetComponent<IAddCamera>() != null)
                    generatedObject.GetComponent<IAddCamera>().AddCamera(cameraForBar);
            }
        }
    }

    ///������� ������������ ������� �� ������
    void DeleteDestroyed()
    {
        foreach (GameObject createObject in createObjects.ToList())
            if (createObject == null)
                createObjects.Remove(createObject);
    }

    Vector3 SpawnPoint()
    {
        //�������� ������� Mesh (����� ��� ����� ������� ���������� ������������� �������). ��� �����, ��� �� �������� ����������� ����� ��������
        float x = (parameters.spawnerLocationX) * Random.Range(-.5f, .5f);
        float z = (parameters.spawnerLocationZ) * Random.Range(-.5f, .5f);

        return (new Vector3(x, 0.5f, z)) + gameObject.transform.position;
    }
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawningDifferentObjectsParameters", menuName = "Apiary/SpawningDifferentObjectsParameters", order = 2)]
public class SpawningDifferentObjectsParameters : ScriptableObject
{
    [SerializeField]
    public float time;
    [SerializeField]
    public int appearsAtTime = 1;
    [SerializeField]
    public int maxNumberObject = 4;
    [Header("Список типа ObjectAndFloat, с объектом и вероятностью")]
    [SerializeField]
    public List<ObjectAndFloat> spawningObject = new List<ObjectAndFloat>();

    [SerializeField]
    public Vector3 areaSpawning;
    [Header("Расстояние от центра до соседнего объекта")]
    [SerializeField]
    public float distance;
    [SerializeField]
    public string neighborLayer;
}

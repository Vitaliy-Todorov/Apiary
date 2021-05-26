using System.Collections.Generic;
using UnityEngine;

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
}

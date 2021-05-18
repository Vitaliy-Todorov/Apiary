using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hive", menuName = "Apiary/Hive", order = 2)]
public class HiveParameters : ScriptableObject
{
    [Header("Parameters")]
    [SerializeField]
    public float timeNewBee;
    [SerializeField]
    public int appearsAtTime;
    [SerializeField]
    public int maxNumberBee;
    [SerializeField]
    public GameObject bee;
    [SerializeField]
    public float maxHoney;
    [Header("Список типа ObjectAndFloat для гинерации, с объектом и вероятностью")]
    [SerializeField]
    public List<ObjectAndFloat> spawningObject;
}

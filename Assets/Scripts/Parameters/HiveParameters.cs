using UnityEngine;

[CreateAssetMenu(fileName = "Hive", menuName = "Hive/Hive", order = 2)]
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
}

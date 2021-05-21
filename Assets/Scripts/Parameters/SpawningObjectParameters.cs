using UnityEngine;


[CreateAssetMenu(fileName = "SpawningObject", menuName = "Apiary/SpawningObject")]
public class SpawningObjectParameters : ScriptableObject
{
    [SerializeField]
    public float time = 1;
    [SerializeField]
    public int appearsAtTime = 1;
    [SerializeField]
    public int maxNumberObject = 4;
    [SerializeField]
    public float spawnerLocationX;
    [SerializeField]
    public float spawnerLocationZ;
    [SerializeField]
    public GameObject createObjectInStance;
    [Header("Расстояние от центра до соседнего объекта")]
    [SerializeField]
    public float distance;
    [SerializeField]
    public string neighborLayer;
}

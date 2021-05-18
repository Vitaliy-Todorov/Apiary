using UnityEngine;

[CreateAssetMenu(fileName = "ObjectAndFloat", menuName = "Apiary/ObjectAndFloat", order = 2)]
public class ObjectAndFloat : ScriptableObject
{
    [SerializeField]
    public GameObject spawnObject;
    [SerializeField]
    public float probs;
}

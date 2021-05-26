using UnityEngine;

[CreateAssetMenu(fileName = "DeathBees", menuName = "Apiary/DeathBees", order = 2)]
public class DeathBeesParameters : ScriptableObject
{
    [SerializeField]
    public float time = 1;
    [SerializeField]
    public int numberObjectsDestroyed = 1;
}

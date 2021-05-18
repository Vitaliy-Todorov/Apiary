using UnityEngine;

[CreateAssetMenu(fileName = "Flower08", menuName = "Apiary/Flower08", order = 2)]
public class FlowerParameters : ScriptableObject
{
    [Header("Parameters")]
    [SerializeField]
    public float maxHoneyStocks = 10;
    [SerializeField]
    public float honeyRecovery = 1;
    [SerializeField]
    public float honeyRecoveryTime = 1;
    [SerializeField]
    public int simultaneousUseByBees = 1;
}

using UnityEngine;

[CreateAssetMenu(fileName = "Flower08", menuName = "Apiary/Flower08")]
public class FlowerParameters : ScriptableObject
{
    [SerializeField]
    public float maxHoneyStocks = 10;
    [SerializeField]
    public float honeyRecovery = 1;
    [SerializeField]
    public float honeyRecoveryTime = 1;
    [SerializeField]
    public int simultaneousUseByBees = 1;
}

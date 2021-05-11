using UnityEngine;

[CreateAssetMenu(fileName = "Bee", menuName = "Bee/Bee", order = 2)]
public class BeesParameters : ScriptableObject
{
    [SerializeField]
    public float speed = 5;
    [SerializeField]
    public float maxHoneyStocks = 8;
    [SerializeField]
    public float getHoney = 4;
    [SerializeField]
    public float getHoneyTime = 1;
}

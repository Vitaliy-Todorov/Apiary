using UnityEngine;

[CreateAssetMenu(fileName = "Bee", menuName = "Apiary/Bee", order = 2)]
public class BeesParameters : GoToParameters
{
    [SerializeField]
    public float maxHoneyStocks = 8;
    [SerializeField]
    public float getHoney = 4;
    [SerializeField]
    public float getHoneyTime = 1;
}

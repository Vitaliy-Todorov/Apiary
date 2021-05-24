using UnityEngine;

[CreateAssetMenu(fileName = "FlyingInsect", menuName = "Apiary/FlyingInsect", order = 2)]
public class FlyingInsectParameters : ScriptableObject, IFlyingInsectParameters
{
    [SerializeField]
    public float speed = 5;

    public float GetSpeed()
    {
        return speed;
    }
}

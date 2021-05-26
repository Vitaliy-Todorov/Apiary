using UnityEngine;

[CreateAssetMenu(fileName = "HiveDweller", menuName = "Apiary/HiveDweller", order = 2)]
public class SpeedParameters : ScriptableObject, IHiveDwellerParameters
{
    [SerializeField]
    public float speed = 5;

    public float GetSpeed()
    {
        return speed;
    }
}

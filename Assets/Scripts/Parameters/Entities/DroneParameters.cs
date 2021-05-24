using UnityEngine;

[CreateAssetMenu(fileName = "Drone", menuName = "Apiary/Drone", order = 2)]
public class DroneParameters : GoToRandomParameters, IGoToParameters
{
    public GameObject GetWeMove()
    {
        return centerOfTrafficArea;
    }
}

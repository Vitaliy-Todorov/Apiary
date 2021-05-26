using UnityEngine;

[CreateAssetMenu(fileName = "Drone", menuName = "Apiary/Drone", order = 2)]
public class DroneParameters : GoToParameters, IGoToRandomParameters
{
    [Header("Высота (y) постоянна")]
    [SerializeField]
    public Vector3 trafficArea = new Vector3(5, 0, 5);

    public GameObject GetCenterOfTrafficArea()
    {
        return weMove;
    }

    public Vector3 GetTrafficArea()
    {
        return trafficArea;
    }
}

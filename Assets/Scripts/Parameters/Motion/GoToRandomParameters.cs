using UnityEngine;

[CreateAssetMenu(fileName = "GoToRandom", menuName = "Apiary/GoToRandom", order = 2)]
public class GoToRandomParameters : SpeedParameters, IGoToRandomParameters
{
    [Header("Высота (y) постоянна")]
    [SerializeField]
    public Vector3 trafficArea = new Vector3(5, 0, 5);
    [SerializeField]
    public GameObject centerOfTrafficArea;

    public GameObject GetCenterOfTrafficArea()
    {
        return centerOfTrafficArea;
    }

    public Vector3 GetTrafficArea()
    {
        return trafficArea;
    }
}

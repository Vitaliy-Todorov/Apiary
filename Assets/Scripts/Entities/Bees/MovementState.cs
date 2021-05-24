using System.Collections.Generic;
using UnityEngine;

public abstract class MovementState
{
    public virtual Vector3 GoTu { get => new Vector3(); }
}


public class GoToHoney : MovementState
{
    GameObject _bee;

    public GoToHoney(GameObject weMove) => _bee = weMove;

    public override Vector3 GoTu
    {
        get => MinDistanceToFlowers(Flowers.freeFlowers);
    }

    Vector3 MinDistanceToFlowers(List<GameObject> distanceTo)
    {
        Vector3 distanceToFlower = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 minDistanceToFlower = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        foreach (GameObject flower in distanceTo)
        {
            if (flower != null)
                distanceToFlower = flower.transform.position - _bee.transform.position;
            if (distanceToFlower.magnitude < minDistanceToFlower.magnitude)
            {
                minDistanceToFlower = distanceToFlower;
            }
        }

        return minDistanceToFlower;
    }
}



/// <summary>
/// Объект bee, будет возвращаться в указанную точку weMove.
/// </summary>
public class GoTo : MovementState
{
    GameObject _bee;
    public GameObject _weMove;

    public GoTo(GameObject bee, GameObject weMove)
    {
        _bee = bee;
        _weMove = weMove;
    }

    public override Vector3 GoTu
    {
        get => _weMove.transform.position - _bee.transform.position;
    }
}



public class GoToRandom : MovementState
{
    GameObject _bee;
    public Vector3 _trafficArea;
    public GameObject _centerOfTrafficArea;

    public GoToRandom(GameObject bee, IGoToRandomParameters parameters)
    {
        _bee = bee;
        _centerOfTrafficArea = parameters.GetCenterOfTrafficArea();
        _trafficArea = parameters.GetTrafficArea();
    }

    public override Vector3 GoTu
    {
        get => SpawnRandomPoint();
    }

    Vector3 SpawnRandomPoint()
    {
        Vector3 offset = new Vector3();

        //Высота постоянна
        offset.y = _trafficArea.y;

        offset.x = _trafficArea.x * UnityEngine.Random.Range(-.5f, .5f);
        offset.z = _trafficArea.z * UnityEngine.Random.Range(-.5f, .5f);

        //Точка в области - координата объекта
        return (offset + _centerOfTrafficArea.transform.position) - _bee.transform.position;
    }
}

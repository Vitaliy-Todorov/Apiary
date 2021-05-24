using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoToRandomParameters: IFlyingInsectParameters
{

    Vector3 GetTrafficArea();
    GameObject GetCenterOfTrafficArea();
}

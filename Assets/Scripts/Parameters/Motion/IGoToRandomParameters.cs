using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoToRandomParameters: IHiveDwellerParameters
{

    Vector3 GetTrafficArea();
    GameObject GetCenterOfTrafficArea();
}

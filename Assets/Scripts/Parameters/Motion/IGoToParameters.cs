using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGoToParameters: IHiveDwellerParameters
{
    void SetWeMove(GameObject weMove);
    GameObject GetWeMove();
}

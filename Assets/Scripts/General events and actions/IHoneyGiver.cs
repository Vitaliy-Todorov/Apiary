using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoneyGiver
{
    bool CanCollectHoney();
    float HoneyGive(GameObject whosAsking, float honey);
}

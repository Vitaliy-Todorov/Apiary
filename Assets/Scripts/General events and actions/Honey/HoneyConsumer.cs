using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HoneyConsumer
{
    IEnumerator ConsumeHoney(IHoneyGiver honeyGiver, float waitTime);
}

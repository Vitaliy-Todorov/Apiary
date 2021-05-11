using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHoneyGetter
{
    IEnumerator HoneyGet(IHoneyGiver honeyGiver, float waitTime);
}

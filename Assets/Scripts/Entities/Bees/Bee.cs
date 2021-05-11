using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    void Start()
    {
        gameObject.AddComponent<MotionBee>();
    }
}

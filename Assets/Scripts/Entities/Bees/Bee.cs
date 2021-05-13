using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public GameObject _hiveThisBee;

    void Start()
    {
        gameObject.AddComponent<MotionBee>();
    }
}

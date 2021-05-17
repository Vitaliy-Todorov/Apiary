using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
    GeneratingObject generatingObject;
    [SerializeField]
    HiveParameters parameters;

    void Start()
    {
        generatingObject = gameObject.AddComponent<GeneratingObject>();
        generatingObject.Init(parameters.timeNewBee, parameters.appearsAtTime, parameters.maxNumberBee, gameObject, parameters.bee);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

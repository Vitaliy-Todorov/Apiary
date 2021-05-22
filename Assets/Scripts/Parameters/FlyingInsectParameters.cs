using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyingInsect", menuName = "Apiary/FlyingInsect", order = 2)]
public class FlyingInsectParameters : ScriptableObject
{
    [SerializeField]
    public float speed = 5;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationHive
{
    GameObject informationHive;

    public InformationHive(GameObject informationHive)
    {
        this.informationHive = informationHive;
    }

    public void OnEnter()
    {
        informationHive.SetActive(true);
    }

    public void OnExit()
    {
        informationHive.SetActive(false);
    }
}

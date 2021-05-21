using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsHive : IState
{
    GameObject actionsHive;

    public ActionsHive(GameObject actionsHive)
    {
        this.actionsHive = actionsHive;
    }

    public void OnEnter()
    {
        actionsHive.SetActive(true);
    }

    public void OnExit()
    {
        actionsHive.SetActive(false);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "GoTo", menuName = "Apiary/GoTo", order = 2)]
public class GoToParameters : FlyingInsectParameters, IGoToParameters
{
    [SerializeField]
    public GameObject weMove;

    public GameObject GetWeMove()
    {
        return weMove;
    }
}

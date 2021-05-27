using UnityEngine;

[CreateAssetMenu(fileName = "GoTo", menuName = "Apiary/GoTo", order = 2)]
public class GoToParameters : SpeedParameters, IGoToParameters
{
    public GameObject GetWeMove()
    {
        throw new System.NotImplementedException();
    }

    public void SetWeMove(GameObject weMove)
    {
        throw new System.NotImplementedException();
    }
}

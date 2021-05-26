using UnityEngine;

[CreateAssetMenu(fileName = "GoTo", menuName = "Apiary/GoTo", order = 2)]
public class GoToParameters : SpeedParameters, IGoToParameters
{
    [SerializeField]
    public GameObject weMove;

    public GameObject GetWeMove()
    {
        return weMove;
    }

    public void SetWeMove(GameObject weMove)
    {
        this.weMove = weMove;
    }
}

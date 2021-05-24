using UnityEngine;

//HoneyConsumer нужен что бы цветок понимал, что у него берут мёд
public class Drone : FlyingInsect, IGeneratedObject
{
    new public DroneParameters parameters;

    public override void GoTo()
    {
        _stateMovement.OnEnterGoTo();
    }

    public override void WorkingGoTo()
    {
        _stateMovement.OnEnterGoToRandom();
    }

    protected void Awake()
    {
        _stateMovement = gameObject.AddComponent<MovementInsect>();
        _stateMovement.Init((IGoToParameters)parameters);
        _stateMovement.Init((IGoToRandomParameters)parameters);
    }
}

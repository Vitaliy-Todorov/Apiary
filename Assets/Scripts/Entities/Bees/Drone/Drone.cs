using UnityEngine;

//HoneyConsumer нужен что бы цветок понимал, что у него берут мёд
public class Drone : HiveDweller
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

    new private void Start()
    {
        base.Start();
        /*
        _stateMovement = gameObject.AddComponent<MovementInsect>();
        _stateMovement.Init((IGoToParameters)parameters);*/
        _stateMovement.Init(WeMove, (IGoToRandomParameters)parameters);
    }
}

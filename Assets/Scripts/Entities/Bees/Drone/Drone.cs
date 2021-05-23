using UnityEngine;

//HoneyConsumer нужен что бы цветок понимал, что у него берут мёд
public class Drone : FlyingInsect, IGeneratedObject
{
    new public DroneParameters parameters;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //Идём к ближайшему свободному цветку
        _stateMovement.OnEnter<GoToRandom>(parameters.trafficArea);
    }
}

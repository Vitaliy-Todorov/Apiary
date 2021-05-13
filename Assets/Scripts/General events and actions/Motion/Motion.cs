using UnityEngine;

public class Motion : MonoBehaviour
{
    protected void Move(Vector3 movement, float speed)
    {
        transform.Translate(movement.normalized * speed * Time.fixedDeltaTime, Space.World);
    }

    /// <summary>
    /// Передвигаем externe в movement со скоростью speed
    /// </summary>
    public static void Move(Transform externe, Vector3 movement, float speed)
    {
        externe.Translate(movement.normalized * speed * Time.fixedDeltaTime, Space.World);
    }

    protected void Teleportation(Vector3 positionTeleportation, Vector3 normalTeleportation)
    {
        transform.position = positionTeleportation;
        transform.forward = normalTeleportation;
    }

    protected void Teleportation(Vector3 positionTeleportation)
    {
        transform.position = positionTeleportation;
    }
}

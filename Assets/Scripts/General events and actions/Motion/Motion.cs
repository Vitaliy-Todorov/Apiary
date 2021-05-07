using UnityEngine;

public class Motion : MonoBehaviour
{
    public void Move(Vector3 movement, float speed)
    {
        transform.Translate(movement * speed * Time.fixedDeltaTime, Space.World);
    }

    public void Teleportation(Vector3 positionTeleportation, Vector3 normalTeleportation)
    {
        transform.position = positionTeleportation;
        transform.forward = normalTeleportation;
    }

    public void Teleportation(Vector3 positionTeleportation)
    {
        transform.position = positionTeleportation;
    }
}

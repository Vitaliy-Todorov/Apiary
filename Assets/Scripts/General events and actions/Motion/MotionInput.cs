using UnityEngine;

public class MotionInput : Motion
{
    [SerializeField]
    float speed = 10.0f;
    float horizontal;
    float vertical;

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        Move(movement, speed);
    }
}

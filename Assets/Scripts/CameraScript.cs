using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Vector3 playerPosition;
    private Vector3 newPosition;
    private readonly Vector3 offset = new Vector3(0, 0, -10);
    private readonly float speed = 0.1f;

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        // This sets the position of the camera to the position of the player
        // Lerp is used because this makes the camera move over time making the movement smoother
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position + offset;
        newPosition = Vector3.Lerp(transform.position, playerPosition, speed);
        transform.position = newPosition;
    }
}
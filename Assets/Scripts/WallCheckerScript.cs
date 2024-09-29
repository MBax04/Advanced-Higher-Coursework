using UnityEngine;

public class WallCheckerScript : MonoBehaviour
{
    private GameObject ground;

    private void Start()
    {
        ground = GameObject.FindGameObjectWithTag("Ground");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Whenever the enmemy walks into a wall the direction is changed
        if (collision.gameObject == ground)
        {
            transform.parent.GetComponent<EnemyScript>().ChangeDirection();
        }
    }
}
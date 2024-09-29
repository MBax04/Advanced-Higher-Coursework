using UnityEngine;

public class GroundCheckerScript : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        // whenever the ground checker leaves ground it calls the parent ChangeDirection method
        transform.parent.GetComponent<EnemyScript>().ChangeDirection();
    }
}
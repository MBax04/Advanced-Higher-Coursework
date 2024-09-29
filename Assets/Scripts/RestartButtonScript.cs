using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    public void RestartGame()
    {
        // When clicker the start screen is loaded
        SceneManager.LoadScene(0);
    }
}
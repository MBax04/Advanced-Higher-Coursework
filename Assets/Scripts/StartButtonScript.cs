using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public void StartGame()
    {
        // When clicked the game is started and the timer is started
        SceneManager.LoadScene(1);
        GameObject.Find("TimerObject").SendMessage("StartTimer");
    }
}
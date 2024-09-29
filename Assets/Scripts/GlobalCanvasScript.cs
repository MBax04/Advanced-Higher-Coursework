using UnityEngine;

public class GlobalCanvasScript : MonoBehaviour
{
    public static GlobalCanvasScript Instance;

    private void Awake()
    {
        // This allows an object to not be destroyed when a new level is loaded
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
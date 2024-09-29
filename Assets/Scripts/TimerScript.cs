using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public static TimerScript Instance;


    public Text timerText;
    private float startTime;

    private float[] times;
    private float time;
    private float minutes;
    private float seconds;
    private string[] levelNumbers;

    private int i;
    private int j;
    private float temp;
    private string temp2;

    private Text[] displayText;

    private int level;

    private float total;
    private bool displayOnce = true;

    private void Awake()
    {
        // This stops the timer object from getting destroyed
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

    private void Start()
    {
        // Some variables are initialised
        times = new float[10];
        displayText = new Text[21];
        levelNumbers = new string[10];

        levelNumbers[0] = "Level 1";
        levelNumbers[1] = "Level 2";
        levelNumbers[2] = "Level 3";
        levelNumbers[3] = "Level 4";
        levelNumbers[4] = "Level 5";
        levelNumbers[5] = "Level 6";
        levelNumbers[6] = "Level 7";
        levelNumbers[7] = "Level 8";
        levelNumbers[8] = "Level 9";
        levelNumbers[9] = "Level 10";
    }

    private void Update()
    {
        TimeLevel();

        // If on the end screen the times are then sorted
        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            SortTimes();
        }
    }

    public void StartTimer()
    {
        // The start time is set to the current time so that the timer is restarted
        startTime = Time.time;
        timerText.enabled = true;
        displayOnce = true;
        total = 0;
    }

    private void TimeLevel()
    {
        // The time is tracked and displayed
        time = Time.time - startTime;

        minutes = Mathf.FloorToInt(time / 60);
        seconds = (time % 60);

        timerText.text = string.Format("{0:00}:{1:00.000}", minutes, seconds);
    }

    public void SaveTime()
    {
        // Before loading the next level the current time for the current level is stored
        level = SceneManager.GetActiveScene().buildIndex;
        times[level - 1] = time;
        GameObject.Find("Player").GetComponent<PlayerScript>().NextLevel2();
    }

    public void SortTimes()
    {
        // The times are sorted using a bubble sort
        for (i = 0; i < 10; i++)
        {
            for (j = 0; j < 9; j++)
            {
                if (times[j] > times[j+1])
                {
                    temp = times[j];
                    times[j] = times[j + 1];
                    times[j + 1] = temp;

                    temp2 = levelNumbers[j];
                    levelNumbers[j] = levelNumbers[j + 1];
                    levelNumbers[j + 1] = temp2;

                }
            }
        }

        DisplayTimes();
    }

    private void DisplayTimes()
    {
        timerText.enabled = false;

        // The different text objects are found and stored
        displayText[0] = GameObject.Find("Text0").GetComponent<Text>();
        displayText[1] = GameObject.Find("Text1").GetComponent<Text>();
        displayText[2] = GameObject.Find("Text2").GetComponent<Text>();
        displayText[3] = GameObject.Find("Text3").GetComponent<Text>();
        displayText[4] = GameObject.Find("Text4").GetComponent<Text>();
        displayText[5] = GameObject.Find("Text5").GetComponent<Text>();
        displayText[6] = GameObject.Find("Text6").GetComponent<Text>();
        displayText[7] = GameObject.Find("Text7").GetComponent<Text>();
        displayText[8] = GameObject.Find("Text8").GetComponent<Text>();
        displayText[9] = GameObject.Find("Text9").GetComponent<Text>();

        displayText[10] = GameObject.Find("Text10").GetComponent<Text>();
        displayText[11] = GameObject.Find("Text11").GetComponent<Text>();
        displayText[12] = GameObject.Find("Text12").GetComponent<Text>();
        displayText[13] = GameObject.Find("Text13").GetComponent<Text>();
        displayText[14] = GameObject.Find("Text14").GetComponent<Text>();
        displayText[15] = GameObject.Find("Text15").GetComponent<Text>();
        displayText[16] = GameObject.Find("Text16").GetComponent<Text>();
        displayText[17] = GameObject.Find("Text17").GetComponent<Text>();
        displayText[18] = GameObject.Find("Text18").GetComponent<Text>();
        displayText[19] = GameObject.Find("Text19").GetComponent<Text>();

        displayText[20] = GameObject.Find("Text20").GetComponent<Text>();

        // All the times are displayed in the correct text object with the correct level number
        for (i = 0; i < 10; i++)
        {
            minutes = Mathf.FloorToInt(times[i] / 60);
            seconds = (times[i] % 60);

            displayText[i].text = string.Format("{0:00}:{1:00.000}", minutes, seconds);
            displayText[i + 10].text = levelNumbers[i];
            total = total + times[i];
        }

        // The times are totalled and a overall time is displayed
        if (displayOnce == true)
        {
            minutes = Mathf.FloorToInt(total / 60);
            seconds = (total % 60);
            displayText[20].text = string.Format("{0:00}:{1:00.000}", minutes, seconds);

            displayOnce = false;
        }
    }
}
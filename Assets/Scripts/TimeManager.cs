using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int passedSeconds;

    private const string LAST_CLOSED_TIME_KEY = "LastClosedTime";

    private void Start()
    {
        // Check if there is a saved last closed time
        if (PlayerPrefs.HasKey(LAST_CLOSED_TIME_KEY))
        {
            string lastClosedTimeString = PlayerPrefs.GetString(LAST_CLOSED_TIME_KEY);
            System.DateTime lastClosedTime = System.DateTime.Parse(lastClosedTimeString);

            // Calculate the time difference
            System.TimeSpan timePassed = System.DateTime.Now - lastClosedTime;

            passedSeconds = timePassed.Seconds;
        }
    }

    private void Update()
    {
        PlayerPrefs.SetString(LAST_CLOSED_TIME_KEY, System.DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // Check if there is a saved last closed time
            if (PlayerPrefs.HasKey(LAST_CLOSED_TIME_KEY))
            {
                string lastClosedTimeString = PlayerPrefs.GetString(LAST_CLOSED_TIME_KEY);
                System.DateTime lastClosedTime = System.DateTime.Parse(lastClosedTimeString);

                // Calculate the time difference
                System.TimeSpan timePassed = System.DateTime.Now - lastClosedTime;

                passedSeconds = timePassed.Seconds;
            }
        }
        else
        {
            PlayerPrefs.SetString(LAST_CLOSED_TIME_KEY, System.DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
    }
}
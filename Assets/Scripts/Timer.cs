
using UnityEngine;
using UnityEngine.UI;
using System;


public class Timer : MonoBehaviour
{
    bool timerActive = false;
    float currentTime;
    public int startMinutes;
    public Text currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;
    }



    // Update is called once per frame
    void Update()
    {
        if (timerActive == true && currentTime > 0)
        {
            currentTime = currentTime - Time.deltaTime;
            if (currentTime <= 0)
            {
                timerActive = false;
                Start();
                Debug.Log("Timer has finished");
            }
        } 

        // Stores time in seconds
        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void StartTimer()
    {
        // for now
        timerActive = !timerActive;
        Debug.Log("Timer Start");

    }

    public void StopTimer()
    {
        timerActive = false;
    }

}

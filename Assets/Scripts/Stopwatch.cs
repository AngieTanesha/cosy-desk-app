using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Stopwatch : MonoBehaviour
{
    // ------------------------------------------------------------------------
    // Variables

    bool StopwatchActive = false;

    // NEED TO INSTANTIATE THIS FIRST. GIRL.
    TimedSessions current = new TimedSessions();

    // Use this as a real-time counter, not to be saved.
    float currentTime;

    // Static - makes one instance in the whole file, which is fine, since this
    // will only need to be called once in the whole game.
    private static List<TimedSessions> sessionsList = new List<TimedSessions>();

    // To get location of desk
    [SerializeField] GameObject desk;

    // To display currentTime
    [SerializeField] Text currentTimeText;


    // ------------------------------------------------------------------------
    // Getters & Setters

    public static List<TimedSessions> GetTimedSessions()
    {
        return sessionsList;
    }

    public static void SetTimedSessions(List<TimedSessions> _times)
    {
        sessionsList = _times;
    }

    // ------------------------------------------------------------------------

    void Start()
    {
        current = new TimedSessions();
        sessionsList = new List<TimedSessions>();
        StopwatchActive = false;


        currentTime = 0;
        currentTimeText.enabled = false;

    }

    // ------------------------------------------------------------------------
    void Update()
    {

        // Show the time elapsed
        if (StopwatchActive == true)
        {
            currentTime += Time.deltaTime;

            // Shows time in hours, minutes, seconds
            TimeSpan time = TimeSpan.FromSeconds(currentTime);

            if (time.Hours <= 0)
            {
                currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
            }
            else
            {
                currentTimeText.text = time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
            }


        }
    }


    // ------------------------------------------------------------------------
   
    public void StartStopwatch()
    {
        StopwatchActive = true;
        currentTimeText.enabled = true;
        
        current.AddStartEndTimes(DateTime.Now);

    }

    // ------------------------------------------------------------------------

    public void StopStopwatch()
    {
        StopwatchActive = false;
        currentTimeText.enabled = false;

        // Resets stopwatch timer
        currentTime = 0;

        // Add start and end times
        current.AddStartEndTimes(DateTime.Now);

        // Add the tags list
        List<string> _tags = TagFiller.GetSelectedTags();
        current.SetTagsList(_tags);


        // Update tags frequency dictionary
        foreach (string selected in _tags)
        {
            if (!TagFiller.tagsFrequency.ContainsKey(selected))
            {
                TagFiller.tagsFrequency.Add(selected, 1);
            } else
            {
                TagFiller.tagsFrequency[selected] += 1;
            }
            
        }

        // Add the TimedSession to file
        sessionsList.Add(current);
    }

    // ------------------------------------------------------------------------

    public void PauseStopwatch()
    {
        StopwatchActive = !StopwatchActive;

        if (StopwatchActive == false)
        {
            Debug.Log("pause");
            currentTimeText.text = "Paused";
        }

        current.AddStartEndTimes(DateTime.Now);

    }

    // ------------------------------------------------------------------------
    // Temporary, show the data.

    public void Show()
    {
        Debug.Log("Graphs " + sessionsList.Count);
        
        foreach (TimedSessions sesh in sessionsList)
        {
  
            List<DateAndTime> temp = sesh.GetStartEndTimes();

            //foreach (DateAndTime hours in temp)
            //{
            //    Debug.Log("Date: " + hours.GetDate());
            //    Debug.Log("Time: " + hours.GetTime());
            //}

            //foreach(string tag in TagFiller.GetSelectedTags())
            //{
            //    Debug.Log(tag);
            //}
        }
    }

}
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TimedSessions
{
    // ------------------------------------------------------------------------
    // Variables

    public List<DateAndTime> startEndTimes;
    public List<string> tagsList;
    public float coinsEarned;

    // Necessary.
    public TimedSessions()
    {
        startEndTimes = new List<DateAndTime>();
        tagsList = new List<string>();
        coinsEarned = 0;
    }

    // ------------------------------------------------------------------------
    // Getters & Setters

    public List<DateAndTime> GetStartEndTimes()
    {
        return startEndTimes;
    }

    public void SetStartEndTimes(List<DateAndTime> _timesList)
    {
        startEndTimes = _timesList;
    }

    // ------------------------------------------------------------------------

    public void AddStartEndTimes(System.DateTime _current)
    {
        DateAndTime _temp = new DateAndTime();
        _temp.datesAndTimes.Add(_current.Date.ToString());
        _temp.datesAndTimes.Add(_current.TimeOfDay.ToString());

        startEndTimes.Add(_temp);
    }

}


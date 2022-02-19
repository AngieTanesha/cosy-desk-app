using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DateAndTime
{
    public List<string> datesAndTimes = new List<string>();

    public string GetDate()
    {
        return datesAndTimes[0];
    }

    public string GetTime()
    {
        return datesAndTimes[1];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimeUI : MonoBehaviour
{
    private TMP_Text timeUIText;

    private void Start()
    {
        timeUIText = this.gameObject.GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        timeUIText.text = GetTimeString(TimeSpan.FromSeconds(Time.time));
    }

    public static string GetTimeString(TimeSpan timeInSeconds)
    {
        string timeString = "";
        string minutestring = "Minutes";
        string secondString = "Seconds";
        if (timeInSeconds.Minutes == 1)
        {
            minutestring = "Minute";
        }
        if (timeInSeconds.Seconds == 1)
        {
            secondString = "Second";
        }
        if (timeInSeconds.Minutes >= 1)
        {
            timeString += string.Format("{0} {1}, ", timeInSeconds.Minutes, minutestring);
        }
        timeString += string.Format("{0} {1}\n", timeInSeconds.Seconds, secondString);
        return timeString;
    }
}

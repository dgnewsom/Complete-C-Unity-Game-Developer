using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WinPanel : MonoBehaviour
{
    [SerializeField] TMP_Text winText;

    internal void SetScoreDisplay(int collisions, float time)
    {
        string winString = "Congratulations\n" +
                           "The Time taken was\n";
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        
        string obstacleString = "Obstacles";
        
        if(collisions == 1)
        {
            obstacleString = "Obstacle";
        }
        winString += TimeUI.GetTimeString(timeSpan);
        winString += string.Format("\nYou hit {0} {1}", collisions, obstacleString);
        winText.text = winString;
        Time.timeScale = 0;
    }
}

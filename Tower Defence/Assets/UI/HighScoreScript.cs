using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreScript : MonoBehaviour
{
    [SerializeField] GameObject newHighScorePanel;
    [SerializeField] TextMeshProUGUI highScoreText;

    public void DisplayNewHighScorePopup()
    {
        newHighScorePanel.SetActive(true);
    }

    internal void SetHighScoreText(int enemiesKilled)
    {
        highScoreText.text = $"High Score : {enemiesKilled}";
    }
}

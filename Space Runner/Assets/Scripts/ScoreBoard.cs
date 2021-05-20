using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_Text scoreDisplay;
    [SerializeField] TMP_Text addScoreDisplay;
    [SerializeField] float scoreAddedDisplayTime = 2f;
    int score;
    float displayTimer = 0f;

    private void Update()
    {
        if (!displayTimer.Equals(0f))
        {
            if(displayTimer < 0f)
            {
                ResetAddScoreDisplay();
            }
            else
            {
                displayTimer -= Time.deltaTime;
            }
        }
    }

    private void ResetAddScoreDisplay()
    {
        addScoreDisplay.text = "";
        displayTimer = 0f;
    }

    public void IncreaseScore(int scoreToAdd)
    {
        score += scoreToAdd;
        DisplayAddScore(scoreToAdd);
        scoreDisplay.text = score.ToString("000000000");
    }

    void DisplayAddScore(int scoreAdded)
    {
        addScoreDisplay.text = $"+{scoreAdded.ToString("000")}";
        displayTimer = scoreAddedDisplayTime;
    }
}

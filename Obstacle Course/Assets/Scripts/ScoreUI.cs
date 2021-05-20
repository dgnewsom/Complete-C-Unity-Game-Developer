using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text scoreUIText;

    private void Start()
    {
        scoreUIText = this.gameObject.GetComponent<TMP_Text>();
    }

    internal void UpdateScore(int collisions)
    {
        string scoreString;
        if(collisions == 1)
        {
            scoreString = collisions + " Hit";
        }
        else
        {
            scoreString = collisions + " Hits";
        }
        scoreUIText.text = scoreString;
    }

    
}

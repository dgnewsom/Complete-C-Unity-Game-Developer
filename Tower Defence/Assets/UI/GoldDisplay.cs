using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldDisplay : MonoBehaviour
{
    TextMeshProUGUI goldDisplay;

    private void Awake()
    {
        goldDisplay = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDisplay(int goldValue)
    {
        if(goldValue < 0)
        {
            goldDisplay.text = "GAME\nOVER!";
        }
        else
        {
            goldDisplay.text = goldValue.ToString();
        }
    }
}

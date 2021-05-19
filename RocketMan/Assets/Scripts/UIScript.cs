using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIScript : MonoBehaviour
{
    [SerializeField] Slider fuelGauge;
    [SerializeField] TMP_Text fuelText;
    [SerializeField] TMP_Text levelName;
    [SerializeField] TMP_Text timer;
    [SerializeField] GameObject endScreen;
    private string timerString;
    TimeSpan timespan;

    private void Awake()
    {
        levelName.text = SceneManager.GetActiveScene().name;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        UpdateTimer();
    }

    public static void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game!");
    }

    private void UpdateTimer()
    {
        timespan = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        timerString = string.Format("{0}:{1}:{2}", timespan.Minutes.ToString("D2"), timespan.Seconds.ToString("D2"), timespan.Milliseconds.ToString("D3").Substring(0,2));
        timer.text = timerString;
    }

    public void SetFuelDisplay(int remainingFuel, int maxFuel)
    {
        float fuelAmount = (float)remainingFuel / maxFuel;
        fuelGauge.value = fuelAmount;
        fuelText.text = string.Format("{0} / {1}", remainingFuel, maxFuel);
    }

    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
    }
}

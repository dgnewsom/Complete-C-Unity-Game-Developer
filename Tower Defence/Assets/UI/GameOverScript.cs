using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] TextMeshProUGUI loseConditionText;
    [SerializeField] TextMeshProUGUI highScoreText;


    int currentHighscore;

    public int CurrentHighscore { get => currentHighscore; set => currentHighscore = value; }

    private void Awake()
    {
        currentHighscore = PlayerPrefs.GetInt("Highscore", 0);
    }

    public void DisplayGameOverScreen(bool coinsNegative)
    {
        int score = FindObjectOfType<Scorer>().EnemiesKilled;
        string loseCondition = "";
        if (coinsNegative)
        {
            loseCondition = "The enemy stole all of your gold!";
        }
        else
        {
            loseCondition = "Your castle was overun!";
        }
        string highscoreString = $"Your Score: {score}\n" +
                                 $"HighScore: {currentHighscore}";
        if(score > currentHighscore)
        {
            highscoreString = $"Congratulations your score of {score}\n is the new Highscore!";
            PlayerPrefs.SetInt("Highscore", score);
        }
        loseConditionText.text = loseCondition;
        highScoreText.text = highscoreString;
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }


}

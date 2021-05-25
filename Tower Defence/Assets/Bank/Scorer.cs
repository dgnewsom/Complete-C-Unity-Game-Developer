using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorer : MonoBehaviour
{
    [SerializeField] int enemiesAllowedBeforeGameOver = 10;
    [SerializeField] int enemiesBeforeSpeedUp = 25;
    

    int enemiesReachedGoal = 0, enemiesKilled = 0, speedUpCounter;

    ObjectPool enemyPool;
    EnemiesKilledDisplay enemiesKilledDisplay;
    ReachedTargetDisplay reachedTargetDisplay;
    GameOverScript gameOverScript;
    HighScoreScript highScoreScript;

    bool gameOver = false;

    public int EnemiesKilled { get => enemiesKilled;}

    private void Start()
    {
        speedUpCounter = enemiesBeforeSpeedUp;
        enemiesKilledDisplay = FindObjectOfType<EnemiesKilledDisplay>();
        reachedTargetDisplay = FindObjectOfType<ReachedTargetDisplay>();
        enemyPool = FindObjectOfType<ObjectPool>();
        gameOverScript = FindObjectOfType<GameOverScript>();
        highScoreScript = FindObjectOfType<HighScoreScript>();
        UpdateEnemyKillDisplay();
        UpdateEnemiesReachedGoalDisplay();
        highScoreScript.SetHighScoreText(gameOverScript.CurrentHighscore);
    }

    public void KillEnemy()
    {
        if (!gameOver)
        {
            enemiesKilled++;
            speedUpCounter--;
            CheckForSpeedUp();
            CheckForHighScore();
            UpdateEnemyKillDisplay();
        }
    }

    private void CheckForHighScore()
    {
        if(enemiesKilled > gameOverScript.CurrentHighscore)
        {
            highScoreScript.SetHighScoreText(enemiesKilled);
            if(enemiesKilled == gameOverScript.CurrentHighscore + 1)
            {
                highScoreScript.DisplayNewHighScorePopup();
            }
        }
    }

    private void CheckForSpeedUp()
    {
        if(speedUpCounter <= 0 && enemyPool != null)
        {
            enemyPool.SpeedUpEnemies();
            speedUpCounter = enemiesBeforeSpeedUp;
        }
    }

    private void UpdateEnemyKillDisplay()
    {
        if(enemiesKilledDisplay != null)
        {
            enemiesKilledDisplay.SetEnemiesKilledDisplay(enemiesKilled);
        }
    }

    public void EnemyReachedGoal()
    {
        if (!gameOver)
        {
            enemiesReachedGoal++;
            UpdateEnemiesReachedGoalDisplay();
            if (enemiesReachedGoal >= enemiesAllowedBeforeGameOver)
            {
                GameOver();
            }
        }
    }

    private void UpdateEnemiesReachedGoalDisplay()
    {
        if(reachedTargetDisplay != null)
        {
            reachedTargetDisplay.SetReachedTargetDisplay(enemiesReachedGoal, enemiesAllowedBeforeGameOver);
        }
    }


    private void GameOver()
    {
        gameOver = true;
        gameOverScript.DisplayGameOverScreen(false);
    }
}

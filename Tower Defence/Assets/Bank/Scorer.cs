using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    [SerializeField] int enemiesAllowedBeforeGameOver = 10;

    int enemiesReachedGoal = 0;
    int enemiesKilled = 0;

    EnemiesKilledDisplay enemiesKilledDisplay;
    ReachedTargetDisplay reachedTargetDisplay;

    private void Start()
    {
        enemiesKilledDisplay = FindObjectOfType<EnemiesKilledDisplay>();
        reachedTargetDisplay = FindObjectOfType<ReachedTargetDisplay>();
        UpdateEnemyKillDisplay();
        UpdateEnemiesReachedGoalDisplay();
    }

    public void KillEnemy()
    {
        enemiesKilled++;
        UpdateEnemyKillDisplay();
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
        enemiesReachedGoal++;
        UpdateEnemiesReachedGoalDisplay();
        if(enemiesReachedGoal >= enemiesAllowedBeforeGameOver)
        {
            GameOver();
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
        throw new NotImplementedException();
    }
}

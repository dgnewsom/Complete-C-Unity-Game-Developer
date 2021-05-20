using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathParticles;
    [SerializeField] int scoreValue = 5;

    ScoreBoard scoreboard;
    bool isDead = false;

    private void Start()
    {
        scoreboard = FindObjectOfType<ScoreBoard>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!isDead)
        {
            isDead = true;
            IncreaseScore();
            KillEnemy();
        }
    }

    private void IncreaseScore()
    {
        scoreboard.IncreaseScore(scoreValue);
    }

    private void KillEnemy()
    {
        Instantiate(enemyDeathParticles, this.transform.position, Quaternion.identity, GameObject.Find("SpawnedItems").transform);
        Destroy(gameObject);
    }
}

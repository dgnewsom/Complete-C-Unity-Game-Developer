using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathParticles;
    [SerializeField] GameObject hitParticles;
    [SerializeField] int hitScoreValue = 500;
    [SerializeField] int deathScoreValue = 5000;
    [SerializeField] int startHealth = 30;
    int currentHealth;
    ScoreBoard scoreboard;
    bool isDead = false;
    GameObject player;
    Transform spawnParent;

    private void Start()
    {
        scoreboard = FindObjectOfType<ScoreBoard>();
        currentHealth = startHealth;
        player = FindObjectOfType<CollisionHandler>().gameObject;
        spawnParent = GameObject.Find("SpawnedItems").transform;
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject vfx = Instantiate(hitParticles, this.transform.position, Quaternion.identity, spawnParent);
        vfx.transform.LookAt(player.transform);
        if (!isDead)
        {
            IncreaseScore(hitScoreValue);
            currentHealth --;
            if(currentHealth <= 0)
            {
                KillEnemy();
            }
        }
    }

    private void IncreaseScore(int scoreToAdd)
    {
        scoreboard.IncreaseScore(scoreToAdd);
    }

    private void KillEnemy()
    {
        isDead = true;
        IncreaseScore(deathScoreValue);
        Instantiate(enemyDeathParticles, this.transform.position, Quaternion.identity, spawnParent);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathParticles;
    [SerializeField] GameObject hitParticles;
    [SerializeField] int hitScoreValue = 5;
    [SerializeField] int startHealth = 30;
    int currentHealth;
    ScoreBoard scoreboard;
    bool isDead = false;
    GameObject player;

    private void Start()
    {
        scoreboard = FindObjectOfType<ScoreBoard>();
        currentHealth = startHealth;
        player = FindObjectOfType<CollisionHandler>().gameObject;
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject vfx = Instantiate(hitParticles, this.transform.position, Quaternion.identity, GameObject.Find("SpawnedItems").transform);
        vfx.transform.LookAt(player.transform);
        if (!isDead)
        {
            IncreaseScore();
            currentHealth --;
            if(currentHealth <= 0)
            {
                KillEnemy();
            }
        }
    }

    private void IncreaseScore()
    {
        scoreboard.IncreaseScore(hitScoreValue);
    }

    private void KillEnemy()
    {
        isDead = true;
        Instantiate(enemyDeathParticles, this.transform.position, Quaternion.identity, GameObject.Find("SpawnedItems").transform);
        Destroy(gameObject);
    }
}

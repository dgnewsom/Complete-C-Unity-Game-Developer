using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0,50)]int poolSize = 5;
    [SerializeField] [Range(0.5f,30f)]float delayBetweenSpawns = 1f;
    [SerializeField] [Range(0, 5f)] float poolSpeed = 1f;
    [SerializeField] [Range(0, 1f)] float speedUpAmount = 0.5f;
    [SerializeField] [Range(0, 5f)] float maxSpeed = 5f;

    SpeedUpPanel speedUpPanel;
    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
        speedUpPanel = FindObjectOfType<SpeedUpPanel>();
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].GetComponent<EnemyMover>().SetSpeed(poolSpeed);
            pool[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawning(delayBetweenSpawns));        
    }

    IEnumerator StartSpawning(float delay)
    {
        while (Application.isPlaying)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(delay);
        }
    }

    private void EnableObjectInPool()
    {
        foreach(GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                enemy.GetComponent<EnemyMover>().SetSpeed(poolSpeed);
                break;
            }
        }
    }

    internal void SpeedUpEnemies()
    {
        if (maxSpeed.Equals(poolSpeed))
        {
            return;
        }
        poolSpeed += speedUpAmount;
        if(poolSpeed > maxSpeed) { poolSpeed = maxSpeed;}
        speedUpPanel.ShowPanel();
        for(int i = 0; i < pool.Length; i++)
        {
            pool[i].GetComponent<EnemyMover>().SetSpeed(poolSpeed);
        }
    }
}

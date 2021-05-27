using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] float startingHealth = 10f;

    float currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"{damageAmount} damage taken!");
        if(currentHealth <= 0f)
        {
            DeathBehaviour();
        }
    }

    private void DeathBehaviour()
    {
        Debug.Log($"{name} is dead!");
        Destroy(gameObject,1f);
    }
}

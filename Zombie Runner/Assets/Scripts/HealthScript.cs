using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] float startingHealth = 10f;

    float currentHealth;
    bool isDead;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public virtual void TakeDamage(float damageAmount)
    {
        if (!isDead)
        {
            currentHealth -= damageAmount;
            Debug.Log($"{name} - {damageAmount} damage taken!");
            if(currentHealth <= 0f)
            {
                DeathBehaviour();
            }
        }
    }

    internal virtual void DeathBehaviour()
    {
        Debug.Log($"{name} is dead!");
        isDead = true;
    }
}

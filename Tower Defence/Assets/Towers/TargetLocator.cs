using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15f;

    ParticleSystem projectileParticles;
    Transform target;

    private void Start()
    {
        projectileParticles = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.transform.position);
        weapon.LookAt(target);
        
        if(targetDistance <= range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    void Attack(bool isAttacking)
    {
        var emission = projectileParticles.emission;
        emission.enabled = isAttacking;
    }
}

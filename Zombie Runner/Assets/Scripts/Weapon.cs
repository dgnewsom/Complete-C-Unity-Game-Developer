using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float damageAmount = 1f;
    [SerializeField] float shootDelay = 0.5f;
    [SerializeField] bool isAutomatic = false; 
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] Camera FPCamera;
    [SerializeField] GameObject hitEffect;

    ParticleSystem muzzleFlash;
    bool canFire = true;

    private void Start()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine(ShootDelay(0.5f));
    }

    void Update()
    {
        if (isAutomatic)
        {
            if (Input.GetButton("Fire1") && canFire)
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && canFire)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            GetComponent<Animation>().Play();
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            canFire = false;
            StartCoroutine(ShootDelay(shootDelay));
        }
    }

    IEnumerator ShootDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canFire = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitEffect(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damageAmount);
            }
        }
        else { return; }
    }

    private void CreateHitEffect(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}

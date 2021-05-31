using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damageAmount = 1f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] float shootDelay = 0.5f;
    [SerializeField] bool isAutomatic = false; 

    bool canFire = true;

    private void Start()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
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
        if (ammoSlot.GetCurrentAmmo() > 0)
        {
            GetComponent<Animation>().Play();
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo();
            canFire = false;
            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
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

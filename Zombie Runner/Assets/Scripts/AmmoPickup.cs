using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammotype;
    [SerializeField] int quantity = 10;
    [SerializeField] Transform[] models;

    private void Awake()
    {
        SetAmmoModel();
    }

    private void SetAmmoModel()
    {
        int ammoTypeInt = (int)ammotype;

        for(int i = 0; i < models.Length; i++)
        {
            if (i.Equals(ammoTypeInt))
            {
                models[i].gameObject.SetActive(true);
            }
            else
            {
                models[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Ammo ammoScript = other.GetComponent<Ammo>();
            if(ammoScript != null)
            {
                PickupAmmo(ammoScript);
            }
            Destroy(gameObject);
        }
    }

    private void PickupAmmo(Ammo ammoScript)
    {
        ammoScript.AddAmmo(ammotype, quantity);
    }
}

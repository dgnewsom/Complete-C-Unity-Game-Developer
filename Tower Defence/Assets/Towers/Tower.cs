using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int costOfTower = 75;

    private void Start()
    {
        
    }

    internal bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank.Equals(null))
        {
            return false;
        }
        
        if(bank.CurrentBalance >= costOfTower)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(costOfTower);
            return true;
        }

        return false;
    }
}

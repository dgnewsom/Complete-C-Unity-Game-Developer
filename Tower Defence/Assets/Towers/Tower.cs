using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int costOfTower = 75;
    [SerializeField] private float buildDelay = 1f;

    private void Start()
    {
        StartCoroutine(BuildTower());
    }

    IEnumerator BuildTower()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
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

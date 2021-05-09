using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    [SerializeField] int fuelAmount = 100;

    public int CollectFuel()
    {
        GameObject.Destroy(this.gameObject);
        return fuelAmount;
    }
}

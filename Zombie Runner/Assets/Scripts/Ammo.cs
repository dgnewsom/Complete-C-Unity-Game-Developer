using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        AmmoSlot slot = GetAmmoSlot(ammoType);
        if(slot != null)
        {
            return slot.ammoAmount;
        }
        else
        {
            return -1;
        }
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        AmmoSlot slot = GetAmmoSlot(ammoType);
        if (slot != null)
        {
            slot.ammoAmount --;
        }
        
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach(AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType.Equals(ammoType))
            {
                return slot;
            }
        }
        return null;
    }

    public void AddAmmo(AmmoType ammoType, int quantity)
    {
        AmmoSlot slot = GetAmmoSlot(ammoType);
        if (slot != null)
        {
            slot.ammoAmount += quantity;
        }
    }
}

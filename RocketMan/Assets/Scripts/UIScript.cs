using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] Slider fuelGauge;
    [SerializeField] TMP_Text fuelText;
    
    public void SetFuelDisplay(int remainingFuel, int maxFuel)
    {
        float fuelAmount = (float)remainingFuel / maxFuel;
        fuelGauge.value = fuelAmount;
        fuelText.text = string.Format("{0} / {1}", remainingFuel, maxFuel);
    }
}

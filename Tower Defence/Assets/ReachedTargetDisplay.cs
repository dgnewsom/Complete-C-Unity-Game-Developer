using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReachedTargetDisplay : MonoBehaviour
{
    TextMeshProUGUI reacheTargetText;

    private void Awake()
    {
        reacheTargetText = GetComponent<TextMeshProUGUI>();
    }

    public void SetReachedTargetDisplay(int reachedTarget, int maxAllowed)
    {
        reacheTargetText.text = $"{reachedTarget}/{maxAllowed}";
    }
}

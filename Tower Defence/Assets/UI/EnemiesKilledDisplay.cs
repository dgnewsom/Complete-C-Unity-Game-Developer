using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesKilledDisplay : MonoBehaviour
{
    TextMeshProUGUI enemiesKilledText;

    private void Awake()
    {
        enemiesKilledText = GetComponent<TextMeshProUGUI>();
    }

    public void SetEnemiesKilledDisplay(int enemiesKilled)
    {
        enemiesKilledText.text = enemiesKilled.ToString();
    }
}

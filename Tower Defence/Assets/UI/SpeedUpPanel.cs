using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPanel : MonoBehaviour
{
    [SerializeField] GameObject speedUpPanel;

    public void ShowPanel()
    {
        speedUpPanel.SetActive(true);
    }
}

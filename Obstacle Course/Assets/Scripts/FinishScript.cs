using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishScript : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            int collisions = collision.gameObject.GetComponent<Scorer>().Collisions;
            float time = Time.time;
            winPanel.SetActive(true);
            winPanel.GetComponent<WinPanel>().SetScoreDisplay(collisions, time);
        }
    }
}

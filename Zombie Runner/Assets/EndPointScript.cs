using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointScript : MonoBehaviour
{
    UIScript uiScript;

    private void Start()
    {
        uiScript = FindObjectOfType<UIScript>();
        if (uiScript == null) { Debug.LogError("No UI script found!"); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Escaped!");
            uiScript.DisplayWinScreen();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthScript
{
    UIScript uiScript;

    private void Start()
    {
        uiScript = FindObjectOfType<UIScript>();
        if(uiScript == null) { Debug.LogError("No UI script found!"); }
    }

    internal override void DeathBehaviour()
    {
        base.DeathBehaviour();
        uiScript.DisplayGameOverScreen();
    }

}

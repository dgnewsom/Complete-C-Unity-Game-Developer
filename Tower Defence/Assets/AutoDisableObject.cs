using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisableObject : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 100f)] float disableDelay = 1f;

    private void OnEnable()
    {
        Invoke(nameof(DisableObject), disableDelay);
    }

    void DisableObject()
    {
        this.gameObject.SetActive(false);
    }
}

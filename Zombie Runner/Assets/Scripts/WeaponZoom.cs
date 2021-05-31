using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{

    [SerializeField] Camera playerCamera;
    [SerializeField] RigidbodyFirstPersonController playerController;
    [SerializeField] bool toggle = false;
    [SerializeField] float zoomedInFOVAmount = 20f;
    [SerializeField] float zoomedOutFOVAmount = 60f;
    [SerializeField] float zoomedOutSensitivity = 2f;
    [SerializeField] float zoomedInSensitivity = 0.5f;

    bool isZoomed = false;

    void Start()
    {
        SetCameraZoom();
    }


    // Update is called once per frame
    void Update()
    {
        if (toggle)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                isZoomed = !isZoomed;
            }
            SetCameraZoom();
        }
        else
        {
            if (Input.GetButton("Fire2"))
            {
                isZoomed = true;
            }
            else
            {
                isZoomed = false;
            }
            SetCameraZoom();
        }
        
    }

    private void SetCameraZoom()
    {
        if (isZoomed)
        {
            playerCamera.fieldOfView = zoomedInFOVAmount;
            SetMouseSensitivity(zoomedInSensitivity);
        }
        else
        {
            playerCamera.fieldOfView = zoomedOutFOVAmount;
            SetMouseSensitivity(zoomedOutSensitivity);
        }
    }
    private void SetMouseSensitivity(float setValue)
    {
        playerController.mouseLook.XSensitivity = setValue;
        playerController.mouseLook.YSensitivity = setValue;
    }
}

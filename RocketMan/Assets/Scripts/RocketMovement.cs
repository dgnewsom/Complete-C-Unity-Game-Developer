using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] float thrustForce = 25f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] int maxFuelAmount = 999;
    [SerializeField] int fuelCost = 1;

    int fuelRemaining;
    Rigidbody rigidBody;
    AudioSource engineSound;
    ParticleSystem engineParticleSystem;
    UIScript uiScript;
    bool isDead = false;

    public bool IsDead { get => isDead; set => isDead = value; }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        engineParticleSystem = GameObject.Find("EngineParticleSystem").GetComponent<ParticleSystem>();
        fuelRemaining = maxFuelAmount;
        uiScript = FindObjectOfType<UIScript>();
        UpdateFuelDisplay();
    }

    internal void KillPlayer()
    {
        SetEngineParticleSystem(false);
        SetEngineSound(false);
    }

    private void UpdateFuelDisplay()
    {
        uiScript.SetFuelDisplay(fuelRemaining, maxFuelAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Thrust();
            Rotate();
        }
    }

    /*
     * Control thrust when space key pressed
     */
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(fuelRemaining - fuelCost >= 0)
            {
                SetEngineSound(true);
                SetEngineParticleSystem(true);
                rigidBody.AddRelativeForce(Vector3.up * thrustForce);
                fuelRemaining = fuelRemaining - fuelCost;
                UpdateFuelDisplay();
            }
        }
        else
        {
            SetEngineSound(false);
            SetEngineParticleSystem(false);
        }
    }



    /*
     * Control rotation when left and right buttons pressed
     */
    private void Rotate()
    {
        rigidBody.freezeRotation = true; //Take manual control of rotation
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; // Return rotation control to physics
    }

    /*
     * Adjust engine sound if boosting.
     */
    private void SetEngineSound(bool isBoosting)
    {
        if (isBoosting)
        {
            engineSound.volume = 1f;
            engineSound.pitch = 1f;
        }
        else
        {
            engineSound.volume = 0.2f;
            engineSound.pitch = 0.6f;
        }
    }

    /*
     * Play engine particles if boosting.
     */
    private void SetEngineParticleSystem(bool isBoosting)
    {
        if (isBoosting)
        {
            engineParticleSystem.Play();
        }
        else
        {
            engineParticleSystem.Stop();
        }
    }

    public void AddFuel(int fuelToAdd)
    {

        print(fuelToAdd + " Fuel Added");
        if (fuelRemaining + fuelToAdd <= maxFuelAmount)
        {
            fuelRemaining += fuelToAdd;
        }
        else
        {
            fuelRemaining = maxFuelAmount;
        }
        UpdateFuelDisplay();
    }
}

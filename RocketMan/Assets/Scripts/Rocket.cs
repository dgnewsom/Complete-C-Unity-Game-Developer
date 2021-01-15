using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    float thrustForce = 25f;
    [SerializeField]
    float rotationSpeed = 100f;
    [SerializeField]
    int fuelAmount = 999;
    [SerializeField]
    int fuelCost = 1;

    int fuelRemaining;
    Rigidbody rigidBody;
    AudioSource engineSound;
    ParticleSystem engineParticleSystem;
    ParticleSystem deathParticleSystem;
    Text fuelText;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        engineParticleSystem = GameObject.Find("EngineParticleSystem").GetComponent<ParticleSystem>();
        deathParticleSystem = GameObject.Find("DeathParticleSystem").GetComponent<ParticleSystem>();
        fuelRemaining = fuelAmount;
        fuelText = GameObject.Find("Fuel Text").GetComponent<Text>();
        SetFuelDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
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
                SetFuelDisplay();
            }
        }
        else
        {
            SetEngineSound(false);
            SetEngineParticleSystem(false);
        }
    }

    private void SetFuelDisplay()
    {
        fuelText.text = fuelRemaining.ToString();
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

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Safe":
                print("Safe collision");
                break;
            default:
                KillPlayer();
                break;
        }
    }

    private void KillPlayer()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        deathParticleSystem.Play();
        SetEngineParticleSystem(false);
        SetEngineSound(false);
        GameObject.Destroy(GameObject.Find("Rocket"),0.5f);

    }
}

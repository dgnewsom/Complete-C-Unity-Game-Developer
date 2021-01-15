using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    float thrustForce = 25f;
    [SerializeField]
    float rotationSpeed = 100f;
    Rigidbody rigidBody;
    AudioSource engineSound;
    ParticleSystem engineParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        engineParticleSystem = GetComponentInChildren<ParticleSystem>();
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
            SetEngineSound(true);
            SetEngineParticleSystem(true);
            rigidBody.AddRelativeForce(Vector3.up * thrustForce);
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
}

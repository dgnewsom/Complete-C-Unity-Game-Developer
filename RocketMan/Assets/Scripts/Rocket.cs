using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrustForce = 25f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] int maxFuelAmount = 999;
    [SerializeField] int fuelCost = 1;
    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip finish;
    [SerializeField] AudioClip pickupSound;

    AudioSource audioSource;
    int fuelRemaining;
    Rigidbody rigidBody;
    ParticleSystem engineParticleSystem;
    ParticleSystem deathParticleSystem;
    UIScript uiScript;
    bool isDead = false;
    bool finished = false;

    public bool IsDead { get => isDead; set => isDead = value; }

    private void Awake()
    {
        isDead = false;
        rigidBody = this.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        engineParticleSystem = GameObject.Find("EngineParticleSystem").GetComponent<ParticleSystem>();
        deathParticleSystem = GameObject.Find("DeathParticleSystem").GetComponent<ParticleSystem>();
        fuelRemaining = maxFuelAmount;
        uiScript = FindObjectOfType<UIScript>();
        UpdateFuelDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fuel":
                int fuelToAdd = other.gameObject.GetComponent<FuelPickup>().CollectFuel();
                AddFuel(fuelToAdd);
                break;
            default:
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Safe":
                print("Safe collision");
                break;
            case "Finish":
                CompleteLevel();
                break;
            default:
                CrashSequence();
                break;
        }
    }

    private void CompleteLevel()
    {
        finished = true;
        audioSource.PlayOneShot(finish);
        Invoke(nameof(LoadNextLevel), 2f);
    }

    internal void CrashSequence()
    {
        isDead = true;
        this.GetComponent<MeshRenderer>().enabled = false;
        SetEngineParticleSystem(false);
        audioSource.PlayOneShot(explosion);
        deathParticleSystem.Play();
        GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Destroy(engineParticleSystem.gameObject);
        Invoke(nameof(RestartLevel), 1f);
    }

    private void UpdateFuelDisplay()
    {
        uiScript.SetFuelDisplay(fuelRemaining, maxFuelAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && !finished)
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
                fuelRemaining -= fuelCost;
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
        audioSource.clip = engineSound;
        audioSource.loop = true;
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (isBoosting)
        {
            audioSource.volume = 1f;
            audioSource.pitch = 1f;
        }
        else
        {
            audioSource.volume = 0.2f;
            audioSource.pitch = 0.6f;
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
        audioSource.PlayOneShot(pickupSound);
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

    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            print("Finished");
        }
        
    }
}

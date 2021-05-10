using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float thrustForce = 25f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] int maxFuelAmount = 999;
    [SerializeField] int generalFuelCost = 1;
    [SerializeField] int boostFuelCost = 2;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip finish;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] bool debugActive;

    private Rigidbody rigidBody;
    private UIScript uiScript;
    private AudioSource audioSource;
    private ParticleSystem boostEngineParticles;
    private ParticleSystem engineParticles;
    private ParticleSystem deathParticles;
    private ParticleSystem leftBoosterParticles;
    private ParticleSystem rightBoosterParticles;
    private int fuelRemaining;
    private float fuelCostTimer = 0;

    private bool isDead = false;
    private bool finished = false;
    private bool collisions = true;
    private bool fuelUsage = true;

    private void Awake()
    {
        isDead = false;
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        boostEngineParticles = GameObject.Find("EngineParticleSystemBoost").GetComponent<ParticleSystem>();
        engineParticles = GameObject.Find("EngineParticleSystem").GetComponent<ParticleSystem>();
        deathParticles = GameObject.Find("DeathParticleSystem").GetComponent<ParticleSystem>();
        leftBoosterParticles = GameObject.Find("LeftBooster").GetComponent<ParticleSystem>();
        rightBoosterParticles = GameObject.Find("RightBooster").GetComponent<ParticleSystem>();
        fuelRemaining = maxFuelAmount;
        uiScript = FindObjectOfType<UIScript>();
        UpdateFuelDisplay();
    }

    void FixedUpdate()
    {
        if (debugActive)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                fuelUsage = !fuelUsage;
                fuelRemaining = maxFuelAmount;
                UpdateFuelDisplay();
                Debug.Log("Fuel usage = " + fuelUsage);
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                collisions = !collisions;
                Debug.Log("Collisions = " + collisions);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadNextLevel();
            }
        }
        if (!isDead && !finished)
        {
            ProcessMovement();
            if (fuelUsage)
            {
                fuelCostTimer += Time.deltaTime;
            }
        }
        if (fuelCostTimer > 2f)
        {
            ReduceGeneralFuel();
        }
    }

    /// <summary>
    /// Process user input and move rocket
    /// </summary>
    private void ProcessMovement()
    {
        ProcessThrust();
        Rotate();
    }

    /// <summary>
    /// Start thrusting when space key is pressed
    /// </summary>
    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (fuelRemaining - boostFuelCost >= 0)
            {
                StartThrusting();
            }
            else
            {
                StopEngineParticles(true);
            }
        }
        else
        {
            StopThrusting();
        }
    }

    /// <summary>
    /// Start Thrusting
    /// </summary>
    private void StartThrusting()
    {
        SetEngineSound(true);
        SetEngineParticleSystem(true);
        rigidBody.AddRelativeForce(Vector3.up * thrustForce);
        if (fuelUsage)
        {
            fuelRemaining -= boostFuelCost;
            UpdateFuelDisplay();
        }
    }

    /// <summary>
    /// Stop thrusting
    /// </summary>
    private void StopThrusting()
    {
        SetEngineSound(false);
        SetEngineParticleSystem(false);
    }

    /// <summary>
    /// Adjust engine sound if rocket boosting
    /// </summary>
    /// <param name="isBoosting">true if boosting</param>
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

    /// <summary>
    /// Play Boos engine particles if thrusting
    /// </summary>
    /// <param name="isBoosting">true if boosting</param>
    private void SetEngineParticleSystem(bool isBoosting)
    {
        if (isBoosting)
        {
            boostEngineParticles.Play();
        }
        else
        {
            boostEngineParticles.Stop();
        }
    }

    /// <summary>
    /// Process rotation of rocket when left and right arrows pressed
    /// </summary>
    private void Rotate()
    {
        if(fuelRemaining >= 0)
        {
            rigidBody.freezeRotation = true; //Take manual control of rotation
            float rotationThisFrame = rotationSpeed * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                RotateLeft(rotationThisFrame);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                RotateRight(rotationThisFrame);
            }
            else
            {
                StopBoosters();
            }
            rigidBody.freezeRotation = false; // Return rotation control to physics
        }
        else
        {
            StopEngineParticles(true);
        }
    }

    /// <summary>
    /// Rotate Rocket right
    /// </summary>
    /// <param name="rotationThisFrame">Rotation amount</param>
    private void RotateRight(float rotationThisFrame)
    {
        leftBoosterParticles.Play();
        transform.Rotate(-Vector3.forward * rotationThisFrame);
    }

    /// <summary>
    /// Rotate Rocket left
    /// </summary>
    /// <param name="rotationThisFrame">Rotation amount</param>
    private void RotateLeft(float rotationThisFrame)
    {
        rightBoosterParticles.Play();
        transform.Rotate(Vector3.forward * rotationThisFrame);
    }

    /// <summary>
    /// Stop booster particles
    /// </summary>
    private void StopBoosters()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }

    /// <summary>
    /// Stop all engine particles
    /// </summary>
    private void StopEngineParticles(bool mainEngine = false)
    {
        boostEngineParticles.Stop();
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
        if (mainEngine)
        {
            engineParticles.Stop();
        }
    }

    /// <summary>
    /// Reduce the fuel level 
    /// </summary>
    private void ReduceGeneralFuel()
    {
        fuelRemaining -= generalFuelCost;
        fuelCostTimer = 0;
        UpdateFuelDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Fuel":
                CollectFuelPickup(other.GetComponent<FuelPickup>());
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Collect Fuel Pickup
    /// </summary>
    /// <param name="pickup">Fuel Pickup Script collected</param>
    private void CollectFuelPickup(FuelPickup pickup)
    {
        int fuelToAdd = pickup.CollectFuel();
        AddFuel(fuelToAdd);
    }
    
    /// <summary>
    /// Add fuel from pickup
    /// </summary>
    /// <param name="fuelToAdd">Amount of fuel collected</param>
    public void AddFuel(int fuelToAdd)
    {
        audioSource.PlayOneShot(pickupSound);
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

    /// <summary>
    /// Update the fuel guage in UI script
    /// </summary>
    private void UpdateFuelDisplay()
    {
        uiScript.SetFuelDisplay(fuelRemaining, maxFuelAmount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisions)
        {
            switch (collision.gameObject.tag)
            {
                case "Safe":
                    if(fuelRemaining <= 0)
                    {
                        Invoke(nameof(RestartLevel), levelLoadDelay);
                    }
                    break;
                case "Finish":
                    CompleteLevel(collision.gameObject.GetComponentInChildren<ParticleSystem>());
                    break;
                default:
                    CrashSequence();
                    break;
            }
        }
    }

    /// <summary>
    /// Play finish sound and particles then load next level after a delay
    /// </summary>
    /// <param name="landingPadParticles">Landing pad particle system</param>
    private void CompleteLevel(ParticleSystem landingPadParticles)
    {
        finished = true;
        landingPadParticles.Play();
        audioSource.PlayOneShot(finish);
        StopEngineParticles(true);
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    /// <summary>
    /// Load next level if it exists. If not finish Game
    /// </summary>
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            print("Finished");
        }

    }

    /// <summary>
    /// Play crash particles and sound then reload current level after a delay.
    /// </summary>
    internal void CrashSequence()
    {
        isDead = true;
        this.GetComponent<MeshRenderer>().enabled = false;
        SetEngineParticleSystem(false);
        audioSource.PlayOneShot(explosion);
        deathParticles.Play();
        GetComponent<Rigidbody>().isKinematic = true;
        StopEngineParticles(true);
        Invoke(nameof(RestartLevel), levelLoadDelay);
    }

    /// <summary>
    /// Restart current level
    /// </summary>
    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

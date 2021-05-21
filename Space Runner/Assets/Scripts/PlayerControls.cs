using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [Header("General Setup Settings")]
    [Tooltip("Ship movement speed between 0 and 60")] [SerializeField] [Range(0, 60)] float MovementSpeed = 15f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 6.5f;

    [Header("Player input controls")]
    [SerializeField] [Tooltip("Delay before activating controls")] float controlDelayTimer = 2.5f;
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;

    [Header("Screen position tuning values")]
    [SerializeField] float positionPitchFactor = -3.5f;
    [SerializeField] float positionYawFactor = 4.5f;

    [Header("Player input tuning values")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -12.5f;

    [Header("Particle Systems")]
    [Tooltip("Add player lasers here")] [SerializeField] GameObject[] lasers;
    [Tooltip("Add Death particles here")] [SerializeField] ParticleSystem deathParticles;

    float xThrow, yThrow;
    bool controlsActive = false;
    bool isDead = false;

    private void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsActive && !isDead)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
        else
        {
            if (controlDelayTimer > 0)
            {
                controlDelayTimer -= Time.deltaTime;
            }
            else
            {
                controlsActive = true;
            }
        }
    }

    private void ProcessFiring()
    {
        //If pushing fire button shoot lasers 
        SetLasersActive(fire.ReadValue<float>() > 0.5f);

    }

    private void SetLasersActive(bool areActive)
    {
        if (areActive)
        {
            Debug.Log("Firing");
        }
        else
        {
            Debug.Log("Stop Firing");
        }

        foreach (GameObject laser in lasers)
        {
            ParticleSystem.EmissionModule emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = areActive;
        }
    }

    private void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * MovementSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = -yThrow * Time.deltaTime * MovementSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchFromPosition = transform.localPosition.y * positionPitchFactor;
        float pitchFromControl = yThrow * controlPitchFactor;
        float pitch = pitchFromPosition + pitchFromControl;


        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;


        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    public void KillPlayer()
    {
        isDead = true;
        SetLasersActive(false);
        GameObject.Find("StarSparrow").SetActive(false);
        deathParticles.Play();
    }
}

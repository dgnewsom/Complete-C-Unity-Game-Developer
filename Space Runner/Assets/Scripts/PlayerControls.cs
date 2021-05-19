using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] [Range(0, 30)] float MovementSpeed = 15f;
    [SerializeField] float positionPitchFactor = -3.5f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 4.5f;
    [SerializeField] float controlRollFactor = -12.5f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 6.5f;
    float xThrow, yThrow;

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField]
    float force = 25f;
    AudioSource rocketEngine;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        rocketEngine = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SetEngineBoostSound(true);
            Boost();
        }
        else
        {
            SetEngineBoostSound(false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            RotateLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            RotateRight();
        }
    }

    private void RotateRight()
    {
        transform.Rotate(-Vector3.forward);
    }

    private void RotateLeft()
    {
        transform.Rotate(Vector3.forward);
    }

    private void Boost()
    {
        rigidbody.AddRelativeForce(0, force, 0);
    }

    private void SetEngineBoostSound(bool isBoosting)
    {
        if (isBoosting)
        {
            rocketEngine.volume = 1f;
            rocketEngine.pitch = 1f;
        }
        else
        {
            rocketEngine.volume = 0.2f;
            rocketEngine.pitch = 0.6f;
        }
    }
}

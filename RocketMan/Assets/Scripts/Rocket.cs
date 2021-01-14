using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField]
    float force = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
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
            Boost();
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
}

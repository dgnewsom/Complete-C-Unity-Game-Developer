using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1;
    // Update is called once per frame
    void Update()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        float yRotation = rotationSpeed * Time.deltaTime * 10;
        transform.Rotate(0, yRotation, 0);
    }
}

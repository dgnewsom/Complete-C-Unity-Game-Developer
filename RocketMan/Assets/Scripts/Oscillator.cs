using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] [Range(0,1)] private float movementFactor;
    [SerializeField] private float cycleLength = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if(cycleLength <= Mathf.Epsilon) { return; } 

        float cycles = Time.time / cycleLength; //Growing with time

        const float tau = Mathf.PI * 2; //Constant value
        float rawSineWave = Mathf.Sin(cycles * tau); //From -1 to 1

        movementFactor = (rawSineWave + 1) / 2; //recalculate 0 to 1
       
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}

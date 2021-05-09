using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] float dropTime = 5f;
    MeshRenderer meshRenderer;
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > dropTime)
        {
            Debug.Log(string.Format("{0} dropped at {1} seconds",gameObject.name, dropTime));
            meshRenderer.enabled = true;
            rigidBody.useGravity = true;
        }
    }
}

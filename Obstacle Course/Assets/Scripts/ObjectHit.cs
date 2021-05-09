using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            ChangeColour();
            gameObject.tag = "Hit";
        }
    }

    private void ChangeColour()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}

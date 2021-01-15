using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform rocket;
    // Start is called before the first frame update
    void Start()
    {
        rocket = GameObject.Find("Rocket").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(rocket.position.x, rocket.position.y +3, -10f);
    }
}

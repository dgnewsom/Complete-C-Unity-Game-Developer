using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    ParticleSystem deathParticleSystem;
    RocketMovement movementScript;

    private void Start()
    {
        deathParticleSystem = GameObject.Find("DeathParticleSystem").GetComponent<ParticleSystem>();
        movementScript = gameObject.GetComponent<RocketMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Safe":
                print("Safe collision");
                break;
            case "Fuel":
                print("Fuel Added");
                int fuelToAdd = collision.gameObject.GetComponent<FuelPickup>().CollectFuel();
                movementScript.AddFuel(fuelToAdd);
                break;
            case "Finish":
                print("Congratulations");
                break;
            default:
                KillPlayer();
                break;
        }
    }

    private void KillPlayer()
    {
        movementScript.KillPlayer();
        this.GetComponent<MeshRenderer>().enabled = false;
        deathParticleSystem.Play();        
        GameObject.Destroy(this.gameObject, 0.5f);

    }
}

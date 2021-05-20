using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathParticles;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"{gameObject.name} hit by {other.gameObject.name}");
        GameObject explosion = Instantiate(enemyDeathParticles,this.transform.position,Quaternion.identity);
        Destroy(explosion, 5f);
        Destroy(gameObject);
    }
}

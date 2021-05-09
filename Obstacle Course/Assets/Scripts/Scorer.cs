using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    int collisions;
    [SerializeField] ScoreUI scoreUI;

    public int Collisions { get => collisions;}

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("Hit"))
        {
            collisions++;
            Debug.Log(string.Format("{0} has hit {1} things!", gameObject.name, collisions));
            scoreUI.UpdateScore(collisions);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower tower;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get => isPlaceable;}


    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced;
            isPlaced = tower.CreateTower(tower, transform.position);
            isPlaceable = !isPlaced;
        }
    }
}

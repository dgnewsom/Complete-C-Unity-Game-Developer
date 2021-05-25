using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower tower;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get => isPlaceable;}

    GridManager gridManager;
    PathFinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isPlaced;
            isPlaced = tower.CreateTower(tower, transform.position);
            isPlaceable = !isPlaced;
            if (!isPlaceable)
            {
               gridManager.BlockNode(coordinates);
            }
        }
    }
}

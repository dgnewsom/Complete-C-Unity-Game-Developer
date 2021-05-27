using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    
    float speed = 1f;
    List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;
    Scorer scorer;


    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        scorer = FindObjectOfType<Scorer>();
    }

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void ReturnToStart()
    {
        
            transform.position = gridManager.GetPositionFronCoordinates(pathFinder.StartCoordinates);
    }

    internal void SetSpeed(float enemySpeed)
    {
        speed = enemySpeed;
    }

    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());

    }
    IEnumerator FollowPath()
    {
        if (path.Count > 0)
        {
            for (int i = 1; i < path.Count; i++)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = gridManager.GetPositionFronCoordinates(path[i].coordinates);
                float travelPercentage = 0f;

                transform.LookAt(endPosition);

                while (travelPercentage < 1f)
                {
                    travelPercentage += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercentage);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        ReachEndOfPath();
    }

    private void ReachEndOfPath()
    {
        enemy.StealGold();
        if(scorer != null)
        {
            scorer.EnemyReachedGoal();
        }
        gameObject.SetActive(false);
    }
}

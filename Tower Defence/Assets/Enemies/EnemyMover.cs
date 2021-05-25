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
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        if (path[0] != null)
        {
            transform.position = gridManager.GetPositionFronCoordinates(pathFinder.StartCoordinates);
        }
    }

    internal void SetSpeed(float enemySpeed)
    {
        speed = enemySpeed;
    }

    private void FindPath()
    {
        path.Clear();
        path = pathFinder.GetNewPath();
    }
    IEnumerator FollowPath()
    {
        if (path.Count > 0)
        {
            for (int i = 0; i < path.Count; i++)
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

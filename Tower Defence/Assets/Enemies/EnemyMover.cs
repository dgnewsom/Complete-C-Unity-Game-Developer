using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0,5f)] float speed = 1f;
    [SerializeField] [Tooltip("List of tiles to follow")]List<Waypoint> path = new List<Waypoint>();
    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
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
            transform.position = path[0].transform.position;
        }
    }

    internal void SetSpeed(float enemySpeed)
    {
        speed = enemySpeed;
    }

    private void FindPath()
    {
        path.Clear();

        Transform parent = GameObject.FindGameObjectWithTag("Path").transform;
        
        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if(waypoint != null)
            {
                 path.Add(waypoint);
            }
        }
    }
    IEnumerator FollowPath()
    {
        if (path.Count > 0)
        {
            foreach (Waypoint waypoint in path)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = waypoint.transform.position;
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
        gameObject.SetActive(false);
    }
}

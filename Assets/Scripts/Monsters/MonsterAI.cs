using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public XROrigin xrOrigin;
    public float detectionRange = 10f;
    public Transform[] waypoints;
    private int currentWayPoint = 0;
    private bool isChasingPlayer = false;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        Wander();
    }

    private void FixedUpdate()
    {
        detectPlayer();
    }

    private void detectPlayer()
    {
        Transform playerTransform = xrOrigin.Camera.transform;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < detectionRange)
        {
            agent.SetDestination(playerTransform.position);
            isChasingPlayer = true;
        }
        else
        {
            if (isChasingPlayer)
            {
                isChasingPlayer = false;
            }
            Wander();
        }
    }

    private void Wander()
    {
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            int newWayPoint;
            do
            {
                newWayPoint = Random.Range(0, waypoints.Length);
            } while (newWayPoint == currentWayPoint);
            
            currentWayPoint = newWayPoint;
            agent.SetDestination(waypoints[currentWayPoint].position);
        }
    }
}

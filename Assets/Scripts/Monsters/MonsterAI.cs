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
    public float attackRange = 1f;
    public Transform[] waypoints;
    private int currentWayPoint = 0;
    private bool isChasingPlayer = false;

    public void AIInit()
    {
        agent = GetComponent<NavMeshAgent>();
        Wander();
    }

    public void DetectPlayer()
    {
        Transform playerTransform = xrOrigin.Camera.transform;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < detectionRange && distanceToPlayer >= attackRange)
        {
            agent.SetDestination(playerTransform.position);
            isChasingPlayer = true;
        }
        else if(distanceToPlayer < attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer >= detectionRange)
        {
            if (isChasingPlayer)
            {
                isChasingPlayer = false;
            }
            Wander();
        }
    }

    private void AttackPlayer()
    {
        // 카메라를 Monster쪽으로 돌림과 동시에 몬스터 포식 애니메이션 재생
        // 카메라 흔들림도 추가하면 좋을 듯
        // 애니메이션이 종료되면 게임오버 씬으로
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

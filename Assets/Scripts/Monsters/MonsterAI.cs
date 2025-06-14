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
        // ī�޶� Monster������ ������ ���ÿ� ���� ���� �ִϸ��̼� ���
        // ī�޶� ��鸲�� �߰��ϸ� ���� ��
        // �ִϸ��̼��� ����Ǹ� ���ӿ��� ������
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

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
    public bool isChasingPlayer = false;
    public bool isAttackingPlayer = false;

    private Coroutine coDeadSceneLoad;
    private WaitForSeconds waitTime = new WaitForSeconds(3f);

    public void AIInit()
    {
        agent = GetComponent<NavMeshAgent>();
        Wander();
    }

    public void DetectPlayer()
    {
        Vector3 playerPos = new Vector3(xrOrigin.Camera.transform.position.x, 0, xrOrigin.Camera.transform.position.z);
        Vector3 flatPos = new Vector3(transform.position.x, 0, transform.position.z);
        float distanceToPlayer = Vector3.Distance(flatPos, playerPos);
        if (distanceToPlayer < detectionRange && distanceToPlayer >= attackRange)
        {
            agent.SetDestination(playerPos);
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
        // ī�޶� Monster������ ������ ���ÿ� ���� ���� �ִϸ��̼� ��� <= MonsterController���� isAttacking�� ������ Animation�� ����ϵ��� ����
        // ī�޶� ��鸲�� �߰��ϸ� ���� �� => Controller�ʿ��� �����ϵ��� �����ϴ� �� ���ƺ���
        isChasingPlayer = false;
        isAttackingPlayer = true;
        // �ִϸ��̼��� ����Ǹ� ���ӿ��� ������ => GameManager�� ���� �� ����. 
        coDeadSceneLoad = StartCoroutine(CoDeadSceneLoad());
    }

    private IEnumerator CoDeadSceneLoad()
    {
        yield return waitTime;
        GameManager.Instance.LoadScene("GameOverScene");
        yield return null;
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

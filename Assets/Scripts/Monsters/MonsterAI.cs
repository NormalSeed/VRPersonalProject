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
    public float attackRange = 2f;
    public Transform[] waypoints;
    private int currentWayPoint = 0;
    public bool isMoving = false;
    public bool isAttackingPlayer = false;
    public bool isHeardSound = false;

    private Vector3 targetPos;

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
            isMoving = true;
        }
        else if(distanceToPlayer < attackRange)
        {
            isMoving = false;
            AttackPlayer();
        }
        else if (distanceToPlayer >= detectionRange)
        {
            Wander();
        }
    }
    private void AttackPlayer()
    {
        // 카메라를 Monster쪽으로 돌림과 동시에 몬스터 포식 애니메이션 재생 <= MonsterController에서 isAttacking에 반응해 Animation을 재생하도록 설정
        // 카메라 흔들림도 추가하면 좋을 듯 => Controller쪽에서 제어하도록 변경하는 게 좋아보임
        isMoving = false;
        isAttackingPlayer = true;
        // 애니메이션이 종료되면 게임오버 씬으로 => GameManager를 통해 씬 변경. 
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

            isMoving = true;
            currentWayPoint = newWayPoint;
            agent.SetDestination(waypoints[currentWayPoint].position);
        }
    }

    public void ChaseSound()
    {
        if (isHeardSound)
        {
            agent.SetDestination(targetPos);
            if (Vector3.Distance(transform.position, targetPos) < 0.5f)
            {
                isHeardSound = false;
            }
        }
    }

    public void OnSoundHeard(Vector3 soundPos)
    {
        targetPos = soundPos;
        isHeardSound = true;
    }
}

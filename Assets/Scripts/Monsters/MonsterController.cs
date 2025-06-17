using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private MonsterAI monsterAI;
    private MonsterModel model;
    private Animator animator;
    private Camera attackCam;

    private void Awake() => Init();

    private void Init()
    {
        monsterAI = GetComponent<MonsterAI>();
        model = GetComponent<MonsterModel>();
        animator = GetComponent<Animator>();
        attackCam = GetComponentInChildren<Camera>();
        monsterAI.AIInit();
        SubscribeEvents();
    }

    private void Start()
    {
        attackCam.gameObject.SetActive(false);
    }

    private void Update()
    {
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        monsterAI.DetectPlayer();
    }

    private void HandleAnimation()
    {
        if (monsterAI.isMoving)
        {
            model.IsMoving.Value = true;
        }
        else
        {
            model.IsMoving.Value = false;
        }

        if (monsterAI.isAttackingPlayer)
        {
            Debug.Log("공격 애니메이션 재생");
            model.IsAttacking.Value = true;
            attackCam.gameObject.SetActive(true);
        }
        else
        {
            model.IsAttacking.Value = false;
            attackCam.gameObject.SetActive(false);
        }
    }

    public void SubscribeEvents()
    {
        model.IsMoving.Subscribe(SetMoveAnimation);
        model.IsAttacking.Subscribe(SetAttackAnimation);
    }

    public void UnsubscribeEvents()
    {
        model.IsMoving.Unsubscribe(SetMoveAnimation);
        model.IsAttacking.Unsubscribe(SetAttackAnimation);
    }

    private void SetMoveAnimation(bool value) => animator.SetBool("IsMoving", value);
    private void SetAttackAnimation(bool value) => animator.SetBool("IsAttacking", value);
}

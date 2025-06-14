using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private MonsterAI monsterAI;
    private MonsterModel model;
    private Animator animator;

    private void Awake() => Init();

    private void Init()
    {
        monsterAI = GetComponent<MonsterAI>();
        model = GetComponent<MonsterModel>();
        animator = GetComponent<Animator>();
        monsterAI.AIInit();
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
        if (monsterAI.isChasingPlayer)
        {
            model.IsMoving.Value = true;
        }
        else
        {
            model.IsMoving.Value = false;
        }

        if (monsterAI.isAttackingPlayer)
        {
            model.IsAttacking.Value = true;
        }
        else
        {
            model.IsAttacking.Value = false;
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

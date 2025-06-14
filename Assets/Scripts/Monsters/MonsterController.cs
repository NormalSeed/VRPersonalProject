using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private MonsterAI monsterAI;

    private void Awake() => Init();

    private void Init()
    {
        monsterAI = GetComponent<MonsterAI>();
        monsterAI.AIInit();
    }

    private void FixedUpdate()
    {
        monsterAI.DetectPlayer();
    }
}

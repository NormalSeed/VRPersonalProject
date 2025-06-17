using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnvisibleMonsterController : MonoBehaviour
{
    public GameObject wanderingMonster;
    public MonsterAI monsterAI;
    private SkinnedMeshRenderer meshRenderer;
    public bool underFlashLight;

    private void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void Update()
    {
        Reveal();
    }

    private void Reveal()
    {
        if (underFlashLight)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 적절한 애니메이션으로 변경 필요
            GameManager.Instance.LoadScene("GameOverScene");
        }
    }
}

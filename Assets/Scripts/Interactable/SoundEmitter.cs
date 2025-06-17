using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float soundRadius = 100f;

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.Instance.PlaySFX(SoundManager.ESfx.SFX_DROP);
        EmitSound();
    }

    public void EmitSound()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, soundRadius);
        foreach (var hit in hits)
        {
            var listner = hit.GetComponent<MonsterAI>();
            if (listner != null)
            {
                listner.OnSoundHeard(transform.position);
            }
        }
    }
}

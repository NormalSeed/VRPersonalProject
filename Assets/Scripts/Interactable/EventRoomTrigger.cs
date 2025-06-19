using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EventRoomTrigger : MonoBehaviour
{
    public GameObject monster;
    public GameObject door;
    public GameObject roomMonster;
    public Light ceilingLight;

    private float initialIntensity;
    private float lightDuration = 20f;
    public bool isCounting;
    public bool isInteractable;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        isInteractable = true;
        isCounting = false;
        initialIntensity = ceilingLight.intensity;
    }

    private void Update()
    {
        if (isCounting && isInteractable)
        {
            DyingLight();
        }
    }

    private void DyingLight()
    {
        float delta = Time.deltaTime / lightDuration;
        ceilingLight.intensity = Mathf.Max(ceilingLight.intensity - initialIntensity * delta, 0f);
        if (ceilingLight.intensity <= 0f)
        {
            GameManager.Instance.LoadScene("GameOverScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isInteractable)
        {
            isCounting = true;
            monster.SetActive(false);
            door.SetActive(true);
            roomMonster.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EventRoomTrigger : MonoBehaviour
{
    public GameObject monster;
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monster.SetActive(false);
            door.SetActive(true);
        }
    }
}

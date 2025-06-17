using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteractor : MonoBehaviour
{
    private XRSocketInteractor socket;
    public EventRoomTrigger eventTrigger;
    public GameObject door;
    public GameObject monster;
    private string keyObjectName = "Key";

    private void Awake() => Init();

    private void Init()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnKeyInserted);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnKeyInserted);
    }

    private void OnKeyInserted(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.name == keyObjectName)
        {
            Debug.Log("열쇠가 소켓에 들어감");

            eventTrigger.isInteractable = false;
            eventTrigger.isCounting = false;
            door.SetActive(false);
            monster.SetActive(true);
        }
    }
}

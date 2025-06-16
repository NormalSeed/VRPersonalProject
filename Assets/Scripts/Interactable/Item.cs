using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Item : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.selectExited.AddListener(PickUp);
    }

    private void PickUp(SelectExitEventArgs args)
    {
        InventoryManager.Instance.AddItem(gameObject);
    }
}

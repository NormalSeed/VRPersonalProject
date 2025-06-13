using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using Unity.XR.CoreUtils;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Transform handTransform;
    private List<GameObject> inventory = new();
    private GameObject[] equipedItem = new GameObject[1];

    private void Awake() => Init();   

    private void Init()
    {
        base.SingletonInit();
    }

    public void AddItem(GameObject item)
    {
        inventory.Add(item);
        EquipItem(item.name);
    }

    public void EquipItem(string itemName)
    {
        GameObject targetItem = inventory.Find(item => item.name == itemName);

        if (targetItem != null)
        {
            inventory.Remove(targetItem);
            equipedItem[0] = targetItem;

            targetItem.transform.SetParent(handTransform);
            targetItem.transform.localPosition = Vector3.zero;
            targetItem.transform.localRotation = Quaternion.identity;
            Rigidbody rb = targetItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
        else
        {
            Debug.Log("아이템을 찾을 수 없음");
        }
    }

    public void UnequipItem(GameObject item)
    {
        if (item.transform.parent == handTransform)
        {
            item.transform.SetParent(null);
            equipedItem[0] = null;

            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            item.SetActive(false);
            inventory.Add(item);
        }
    }
}

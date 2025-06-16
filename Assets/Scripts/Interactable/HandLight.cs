using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLight : Item 
{
    public float maxDistance = 5f;
    public LayerMask unvisibleMonster;
    
    private UnvisibleMonsterController lastHitController;


    private void Update()
    {
        RayCast();
    }

    // 손전등 빛에 닿았을 때 상호작용을 위해 RayCast 사용
    private void RayCast()
    {
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, unvisibleMonster))
        {
            var controller = hit.collider.GetComponent<UnvisibleMonsterController>();
            if (controller != null)
            {
                controller.underFlashLight = true;

                if (lastHitController != null && lastHitController != controller)
                    lastHitController.underFlashLight = false;

                lastHitController = controller;
                Debug.Log("드러났다");
            }
        }
        else
        {
            if (lastHitController != null)
            {
                lastHitController.underFlashLight = false;
                lastHitController = null;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 direction = transform.up;

        Gizmos.DrawRay(transform.position, direction * maxDistance);
    }

}

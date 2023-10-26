using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WireGrabbableInteractable : GrabbableInteractable
{
    [SerializeField] private bool heldBySlot = false;
    public override bool Interact(PlayerInteractionManager origin)
    {
        base.Interact(origin);
        heldBySlot = false;
        return true;
    }
    public override void Interact(ObjectSlotInteractable slot)
    {
        grabPosT = slot.transform;
        transform.SetPositionAndRotation(grabPosT.position, grabPosT.rotation);
        if(canBeSetKinematicBySlot)
        {
            rb.isKinematic = true;
        }
        ChangeGrabState(true);
        heldBySlot = true;
    }

    public override void FixedUpdate()
    {
        if (!isGrabbed) return;
        if (!heldBySlot) base.FixedUpdate();
        else
        {
            transform.position = grabPosT.position;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}

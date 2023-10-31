using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlidingPuzzleGrabbableInteractable : GrabbableInteractable
{
    public event System.Action OnRelease = delegate { };
    //FIXME RB Constraints are World, not Local 
    public override void Awake()
    {
        base.Awake();
    }
    public override void FixedUpdate()
    {
        if (!isGrabbed) return;

        desiredVelocity = GetDesiredVelocity();
        
        rb.velocity = GetFinalVelocity(desiredVelocity);
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
    }
    public override void ChangeGrabState(bool newState)
    {
        base.ChangeGrabState(newState);
        if (!newState) OnRelease.Invoke();
    }
    protected override Vector3 GetDesiredVelocity()
    {
        Vector3 moveDir = GetScreenSelfToMouse();
        moveDir.z = 0f; //removes local Z component to get 2D movement (local to transform.parent)
        return transform.parent.TransformVector(moveDir); //moveDir is interpreted as local to transform.parent and transformed into world-space
    }
}

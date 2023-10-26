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
        //Debug.DrawRay(transform.position, GetDesiredVelocity(), Color.white, 3);

        toGrabPos = GetDesiredVelocity(); //Vector3.ProjectOnPlane(grabPosT.position - rb.position, transform.forward);
        
        rb.velocity = GetFinalVelocity();
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
    }
    public override void ChangeGrabState(bool newState)
    {
        base.ChangeGrabState(newState);
        if (!newState) OnRelease.Invoke();
    }
    protected override Vector3 GetDesiredVelocity()
    {
        Vector3 moveDir =(Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position));
        moveDir.z = 0f;
        return transform.parent.TransformVector(moveDir);
    }
//Vector3.ProjectOnPlane(grabPosT.position - rb.position, transform.up);
    //protected override Vector3 GetFinalVelocity() => toGrabPos.normalized * grabSpeedMultiplier;

}

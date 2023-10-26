using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ConstrainedGrabbableInteractable : GrabbableInteractable
{    
    //FIXME RB Constraints are World, not Local 
    public override void Awake()
    {
        base.Awake();
    }
    public override void FixedUpdate()
    {
        if (!isGrabbed) return;
        //base.FixedUpdate();

        toGrabPos = GetDesiredVelocity(); //Vector3.ProjectOnPlane(grabPosT.position - rb.position, transform.forward);
        Debug.DrawRay(transform.position, toGrabPos, Color.white, 3);

        rb.velocity = GetFinalVelocity();
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
        //print("toGrab: " + Vector3.Project(grabPosT.position - rb.position, transform.up));
        //print("speed:" + rb.velocity);
    }
    protected override Vector3 GetDesiredVelocity() => Vector3.Project(transform.parent.TransformVector(Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)), transform.up);
    //protected override Vector3 GetFinalVelocity() => toGrabPos.normalized * grabSpeedMultiplier;
}

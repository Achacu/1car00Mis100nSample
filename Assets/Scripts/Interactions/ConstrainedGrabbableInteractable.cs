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

        desiredVelocity = GetDesiredVelocity();
        //Debug.DrawRay(transform.position, desiredVelocity, Color.white, 3);

        rb.velocity = GetFinalVelocity(desiredVelocity);
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
        //print("toGrab: " + Vector3.Project(grabPosT.position - rb.position, transform.up));
        //print("speed:" + rb.velocity);
    }

    //Grabbed obj moves in its transform.up axis. The amount is determined by the delta between mousePosition and screen position of the obj, converted to world-space  
    protected override Vector3 GetDesiredVelocity()
    {
        Debug.DrawRay(PlayerCam.cam.transform.position, GetScreenSelfToMouse(), Color.white);
        Debug.DrawRay(transform.parent.position, transform.parent.TransformVector(GetScreenSelfToMouse()), Color.gray);
        Debug.DrawRay(transform.parent.position, Vector3.Project(transform.parent.TransformVector(GetScreenSelfToMouse()), transform.up), Color.black);
        
        return Vector3.Project(transform.parent.TransformVector(GetScreenSelfToMouse()), transform.up);
    }
}

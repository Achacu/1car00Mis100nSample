using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FixedRotatingGrabbableInteractable : ConstrainedGrabbableInteractable
{
    [SerializeField] private float rotatedAmount = 0f;
    [SerializeField] private float minTurnedDegrees = 500f;
    public override void Awake()
    {
        base.Awake();
        lastDir = transform.up;
    }    
    public override void FixedUpdate()
    {
        if (!isGrabbed || (rotatedAmount >= minTurnedDegrees)) return;
        toGrabPos = transform.parent.TransformVector(GetDesiredVelocity());


        //print("mouse: "+Input.mousePosition +"valve: "+ Camera.main.WorldToScreenPoint(transform.position));
        //print("toGrabPos: " + toGrabPos);
        //Debug.DrawRay(transform.position, toGrabPos, Color.white, 2);

        transform.rotation = Quaternion.LookRotation(transform.forward, toGrabPos.normalized);
        rotatedAmount -= Vector3.SignedAngle(transform.up, lastDir, transform.forward);
        lastDir = transform.up;
        if (rotatedAmount >= minTurnedDegrees) FlagManager.Instance.SetFlag(FlagName.PipeValvePuzzleDone, true);
    }    
    private Vector3 lastDir;
    protected override Vector3 GetDesiredVelocity()
    {
        //FIXME CAMERA.MAIN CALL
        Vector3 vect = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position));
        vect.x *= -1;
        return vect;
    }
    protected override RigidbodyConstraints GetConstraintsWhenHeld() => constraints;
}

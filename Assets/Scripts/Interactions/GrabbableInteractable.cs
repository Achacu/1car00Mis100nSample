using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableInteractable : Interactable
{    
    public ObjectSlotInteractable.SlotType slotType;

    protected Rigidbody rb;
    protected Collider col;

    protected bool isGrabbed = false;
    protected PlayerInteractionManager holder;
    protected Transform grabPosT;
    protected Vector3 desiredVelocity;
    protected float sqrdDstToGrabPos;

    protected bool useGravity = false;
    protected RigidbodyConstraints constraints;

    [SerializeField] protected float grabSpeedMultiplier = 20f;
    [SerializeField] protected float maxSqrdDstToGrabPosUntilDrop = 500f; // Set this to a low value to auto-ungrab things
    [SerializeField] protected float maxSpeed = 500f; // Set this to a low value to auto-ungrab things
    [SerializeField] protected float grabDst = 1.5f;
    [SerializeField] public bool allowGrabSwap = true;
    [SerializeField] public bool dragAndDrop = false;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        useGravity = rb.useGravity;
        constraints = rb.constraints;

        //if(maxSqrdDstToGrabPosUntilDrop < 400f) Debug.LogError(this.name);
        //if(maxSpeed < 400f) Debug.LogError(this.name);
    }
    public override bool Interact(PlayerInteractionManager origin)
    {
        if (!base.Interact(origin)) return false;
        holder = origin;

        grabPosT = holder.GetGrabPosT();
        holder.SetGrabDst(grabDst);

        ChangeGrabState(true);
        holder.CurrentGrabbedInteractable = this;
        rb.isKinematic = false;        
        //rb.position = grabPosT.position;

        return true;
    }
    public virtual void Interact(ObjectSlotInteractable slot)
    {
        grabPosT = slot.transform;
        rb.isKinematic = true;
        transform.SetPositionAndRotation(grabPosT.position, grabPosT.rotation);
        ChangeGrabState(true);
    }

    public virtual void ChangeGrabState(bool newState) //0:dropped 1:grabbed 
    {
        isGrabbed = newState;
        rb.constraints = newState ? GetConstraintsWhenHeld() : constraints;
        rb.useGravity = newState ? false : useGravity;
        gameObject.layer = newState ? LayerMask.NameToLayer("Ignore Raycast") : LayerMask.NameToLayer("Interactable");
    }

    protected virtual RigidbodyConstraints GetConstraintsWhenHeld()
    {
        return RigidbodyConstraints.FreezeRotation;
    }

    public virtual void FixedUpdate()
    {
        if (!isGrabbed) return;
        desiredVelocity = GetDesiredVelocity();
        sqrdDstToGrabPos = desiredVelocity.sqrMagnitude;
        
        if (sqrdDstToGrabPos > maxSqrdDstToGrabPosUntilDrop)
        {
            holder.CurrentGrabbedInteractable = null;
            return;
        }
        rb.velocity = GetFinalVelocity(desiredVelocity);
        if (rb.velocity.magnitude > maxSpeed) rb.velocity = rb.velocity.normalized * maxSpeed;
    }
    protected virtual Vector3 GetDesiredVelocity() => (grabPosT.position - rb.position);
    protected virtual Vector3 GetFinalVelocity(Vector3 desiredVelocity) => desiredVelocity * grabSpeedMultiplier;

    //helper method
    protected Vector3 GetScreenSelfToMouse() => Input.mousePosition - PlayerCam.cam.WorldToScreenPoint(transform.position);
}

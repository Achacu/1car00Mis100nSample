using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteractionManager : Singleton<PlayerInteractionManager>
{
    [SerializeField] public LayerMask interactMask;
    [SerializeField] public LayerMask obstructionMask;
    [SerializeField] public Transform cameraTransform;
    [SerializeField] public float maxInteractRayDst = 3f;
    [SerializeField] private PlayerInput playerInput;

    private GrabbableInteractable currentGrabbedInteractable;
    public GrabbableInteractable CurrentGrabbedInteractable
    {
        get => currentGrabbedInteractable;
        set
        {
            Rigidbody heldRB = currentGrabbedInteractable?.GetComponent<Rigidbody>();
            DropHeldObj();
            if (heldRB && value && value.allowGrabSwap)
            {
                Rigidbody newHeldRB = value.GetComponent<Rigidbody>();
                Vector3 varPos = heldRB.position;
                heldRB.position = newHeldRB.position;
                newHeldRB.position = varPos;
            }
            currentGrabbedInteractable = value;
        }
    }
    [SerializeField] private Transform grabPosT;

    public void OnEnable()
    {
        playerInput.Controls.General.Interact.performed += PressedInteract;
        playerInput.Controls.General.Interact.canceled += ReleasedInteract;
    }


    public void OnDisable()
    {
        playerInput.Controls.General.Interact.performed -= PressedInteract;
        playerInput.Controls.General.Interact.canceled -= ReleasedInteract;
    }

    private void ReleasedInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //print("released!");
        if (currentGrabbedInteractable && currentGrabbedInteractable.dragAndDrop)
            DropHeldObj();
    }
    private void PressedInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Interactable interactable = RaycastForInteractable();
        if (interactable != null)
        {
            interactable.Interact(this);
        }
        else
        {
            DropHeldObj();
        }
    }
    public Interactable RaycastForInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxInteractRayDst, interactMask))
        {
            RaycastHit hit2;
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            //Check if interactable is obstructed
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit2, hit.distance, obstructionMask, QueryTriggerInteraction.Ignore))
                return null;

            return interactable;            
        }
        else return null;
    } 
    private void DropHeldObj()
    {
        if (currentGrabbedInteractable)
        {
            currentGrabbedInteractable.ChangeGrabState(false);
            currentGrabbedInteractable = null;
        }
    }
    public void GrabObject(GrabbableInteractable objToGrab)
    {
        objToGrab.Interact(this);
    }
    public void SetGrabDst(float dst)
    {
        grabPosT.position = cameraTransform.position + cameraTransform.forward * dst;
    }
    public void Update()
    {
        //print(currentGrabbedInteractable?.gameObject.name);
    }

    public Transform GetGrabPosT() => grabPosT;
}

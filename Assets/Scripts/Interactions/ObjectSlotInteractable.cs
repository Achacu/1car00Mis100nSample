using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlotInteractable : Interactable
{
    public enum SlotType
    {
        None,

        RedWirePlug,
        BlueWirePlug,
        GreenWirePlug,

        PhotoSlot,
        HardDriveTopLeft,
        HardDriveTopCenter,
        HardDriveTopRight,
        HardDriveBottomLeft,
        HardDriveBottomCenter,
        HardDriveBottomRight,
        AbacusBall,

        PotatoEye1,
        PotatoEye2,
        PotatoEye3,
        PotatoBrow1,
        PotatoBrow2,
        PotatoBrow3,
        PotatoBrow4,
        PotatoNose1,
        PotatoNose2,
        PotatoMouth1,
        PotatoMouth2,
    }

    public System.Action<ObjectSlotInteractable, bool> OnSlotCorrectChange = delegate { };

    [SerializeField, Header("Slot config")] private SlotType correctSlotType;
    [SerializeField] private SlotType[] allowedSlotTypes;

    [SerializeField, Header("Debugging")] private bool isCorrectObjectInSlot = false;
    [SerializeField] private GrabbableInteractable slottedObj;

    public override void Awake()
    {
        base.Awake();
    }

    public override bool Interact(PlayerInteractionManager origin)
    {
        if (!base.Interact(origin)) return false;

        GrabbableInteractable grabbedObj = origin.CurrentGrabbedInteractable;
        if (!grabbedObj)
        {
            if (!slottedObj)
            {
                //print("nothing to slot and nothing slotted");
            }
            else
            {
                //print("taken from slot");
                OnSlotCorrectChange.Invoke(this, false);
                TakeFromSlot(origin);
            }
            return true;
        }

        if (grabbedObj.slotType == correctSlotType)
        {
            PlaceInSlot(origin, grabbedObj);
            //print("correct slot type!");
            OnSlotCorrectChange.Invoke(this, true);
            return true;
        }
        for (int i = 0; i < allowedSlotTypes.Length; i++)
        {
            if (grabbedObj.slotType == allowedSlotTypes[i])
            {
                PlaceInSlot(origin, grabbedObj);
                //print("allowed slot type");
                return true;
            }
        }
        //print("incorrect slot type");
        return true;
    }
    public void TakeFromSlot(PlayerInteractionManager origin)
    {
        origin.GrabObject(slottedObj);
        slottedObj = null;
    }
    public void PlaceInSlot(PlayerInteractionManager origin, GrabbableInteractable objToBePlaced)
    {
        if (slottedObj) origin.GrabObject(slottedObj); //makes player pick up the slotted objects and drop the one to be placed
        else origin.CurrentGrabbedInteractable = null; //makes player drop the one to be placed

        objToBePlaced.Interact(this); //picks up the one the player dropped
        slottedObj = objToBePlaced;
    }
}

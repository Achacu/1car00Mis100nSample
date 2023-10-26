using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireHeldObjInteractable : EventSenderCompatibleInteractable
{
    [SerializeField] protected GrabbableInteractable requiredGrabbableObj;

    public override void Awake()
    {
        base.Awake();
    }

    public override bool Interact(PlayerInteractionManager origin)
    {
        if ((requiredGrabbableObj != null) && (origin.CurrentGrabbedInteractable != requiredGrabbableObj))
        {
            print(origin.CurrentGrabbedInteractable?.gameObject.name + " not valid. Requires " + requiredGrabbableObj.gameObject.name);
            // Can't interact as current grabbed interactable is not valid.
            return false;
        }
        // Parent says we cant interact, so passing
        if (!base.Interact(origin)) return false;

        return true;
    }
}

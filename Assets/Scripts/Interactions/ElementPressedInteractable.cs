using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPressedInteractable : EventSenderCompatibleInteractable
{
    public event Action OnElementPressed = delegate { };

    public override bool Interact(PlayerInteractionManager origin)
    {
        if (!base.Interact(origin)) return false;

        //print("interacted");
        OnElementPressed();
        return true;
    }
}

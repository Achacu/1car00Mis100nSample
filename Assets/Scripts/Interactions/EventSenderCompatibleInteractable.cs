using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSenderCompatibleInteractable : Interactable
{
    public EventSender eventSender;

    public override void OnValidate()
    {
        base.OnValidate();
        if (!eventSender) eventSender = GetComponent<EventSender>();
    }

    public override bool Interact(PlayerInteractionManager origin)
    {
        if (!base.Interact(origin)) return false;

        if (!eventSender)
        {
            Debug.LogWarning($"Interactable script of {gameObject.name} not wired to EventSender");
            return false;
        }

        eventSender.TriggerEvent(true);
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSender : MonoBehaviour
{
    public Action<EventSender> OnActivate = delegate { };
    public Action<EventSender> OnDeactivate = delegate { };

    [SerializeField, ContextMenuItem("TestEvent", "TestEvent")] private bool singleUse = false;
    public void TestEvent() => TriggerEvent(true);

    private bool hasBeenUsed = false;
    public void TriggerEvent(bool on)
    {
        if (singleUse && hasBeenUsed) return;

        if (on) OnActivate.Invoke(this);
        else OnDeactivate.Invoke(this);
        hasBeenUsed = true;
    }
}

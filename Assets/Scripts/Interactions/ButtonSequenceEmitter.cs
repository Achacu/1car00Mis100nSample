using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSequenceEmitter : RequireHeldObjInteractable
{
    [SerializeField] string keyToEmit = "null key";
    [SerializeField] ButtonSequenceReactor reactor;

    public override void Awake()
    {
        base.Awake();
        if (reactor == null)
        {
            Debug.LogError($"ButtonSequenceEmitter for key {keyToEmit} does not have a reactor attached!");
        }
    }

    public override bool Interact(PlayerInteractionManager origin)
    {
        if (!base.Interact(origin)) return false;
        reactor.AddToButtonSequence(keyToEmit);
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCheckerInteractable : Interactable
{
    [SerializeField]
    private FlagName flagToSet = FlagName.NullFlag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool Interact(PlayerInteractionManager origin)
    {
        if (!base.Interact(origin)) return false;
        FlagManager.Instance.SetFlag(flagToSet, true);
        return true;
    }
}

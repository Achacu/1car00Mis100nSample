using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool disableOnInteract = false;
    [SerializeField] public TypeOfInteraction typeOfInteraction = TypeOfInteraction.Press;

    public virtual void OnValidate()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Interactable")) gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    public virtual void Awake()
    {
        OnValidate();
    }

    public virtual bool Interact(PlayerInteractionManager origin)
    {
        //print($"{origin.name} interacted with {this.name}");
        if (disableOnInteract) gameObject.SetActive(false);

        return true;
    }
}

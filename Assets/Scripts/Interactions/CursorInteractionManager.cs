using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeOfInteraction
{
    Press,
    Grab,
    Pet,
    None,
}

[RequireComponent(typeof(PlayerInteractionManager))]
public class CursorInteractionManager : Singleton<CursorInteractionManager>
{
    public static event Action<TypeOfInteraction> OnObjectInInteractionRange = delegate { };
    
    private TypeOfInteraction storedLastTypeOfInteraction = TypeOfInteraction.None;
    Coroutine runningCoroutine = null;

    [SerializeField] float uiInteractionPollingRate = 0.1f;

    void Start()
    {
        if (runningCoroutine == null)
        {
            runningCoroutine = StartCoroutine(StartCounting());
        }
    }

    private IEnumerator StartCounting()
    {
        while (true)
        {
            // Check for Interactable object in front of the camera
            Interactable targetInteractable = PlayerInteractionManager.Instance.RaycastForInteractable();

            // If the type of interaction that we want to display in the UI is different from the one that we had before, emit the event.
            TypeOfInteraction targetTypeOfInteraction = targetInteractable != null ? targetInteractable.typeOfInteraction : TypeOfInteraction.None;
            if (targetTypeOfInteraction != storedLastTypeOfInteraction)
            {                
                OnObjectInInteractionRange(targetTypeOfInteraction);
                storedLastTypeOfInteraction = targetTypeOfInteraction;
            }

            // Wait for the appropriate seconds
            yield return new WaitForSeconds(uiInteractionPollingRate);
        }
    }
}

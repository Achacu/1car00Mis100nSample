using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class CursorChanger : MonoBehaviour
{
    [SerializeField] private Texture2D regularCursor;
    [SerializeField] private Texture2D interactableCursor;
    [SerializeField] private Texture2D duringInteractionCursor;

    private RawImage cursorOnScreen;

    void Awake()
    {
        cursorOnScreen = GetComponent<RawImage>();
    }

    private void OnObjectInInteractionRange(TypeOfInteraction typeOfInteraction)
    {
        switch (typeOfInteraction)
        {
            case TypeOfInteraction.None:
                cursorOnScreen.texture = regularCursor;
                break;
            case TypeOfInteraction.Grab:
                cursorOnScreen.texture = interactableCursor;
                break;
            case TypeOfInteraction.Press:
                cursorOnScreen.texture = interactableCursor;
                break;
            default:
                cursorOnScreen.texture = regularCursor;
                break;
        }
    }

    public void OnEnable()
    {
        CursorInteractionManager.OnObjectInInteractionRange += OnObjectInInteractionRange;
    }
    public void OnDisable()
    {
        CursorInteractionManager.OnObjectInInteractionRange -= OnObjectInInteractionRange;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneManager : MonoBehaviour
{
    [SerializeField] ButtonSequenceReactor reactor;

    void OnValidate()
    {
        reactor = GetComponentInChildren<ButtonSequenceReactor>();
        if (reactor == null)
        {
            Debug.LogError("There's not a sequence reactor in the children of this xylophone puzzle. There will be errors!");
        }
    }

    void Awake()
    {
        OnValidate();
    }

    void OnSequenceCompleted(List<string> solution)
    {
        FlagManager.Instance.SetFlag(FlagName.XylophonePuzzleDone, true);
    }

    public void OnEnable()
    {
        reactor.OnSequenceCompleted += OnSequenceCompleted;
    }
    public void OnDisable()
    {
        reactor.OnSequenceCompleted -= OnSequenceCompleted;
    }
}

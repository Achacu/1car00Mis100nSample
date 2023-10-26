using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSequenceReactor : MonoBehaviour
{
    [SerializeField] bool singleUseSequence = true;
    [SerializeField] List<string> targetButtonSequence = new List<string>();
    [SerializeField] List<string> currentButtonSequence = new List<string>();

    public event Action<List<string>> OnSequenceCompleted = delegate { };

    private bool isSequenceCompletedForTheFirstTime = false;

    void OnValidate()
    {
        currentButtonSequence = new List<string>();
    }

    public void AddToButtonSequence(string key)
    {
        if (isSequenceCompletedForTheFirstTime && singleUseSequence) return;

        currentButtonSequence.Add(key);
        if (currentButtonSequence.Count > targetButtonSequence.Count)
        {
            currentButtonSequence = currentButtonSequence.GetRange(currentButtonSequence.Count - targetButtonSequence.Count, currentButtonSequence.Count - 1);
        }
        if (areListEqual(targetButtonSequence, currentButtonSequence))
        {
            OnSequenceCompleted(targetButtonSequence);
            isSequenceCompletedForTheFirstTime = true;
        }
    }

    bool areListEqual(List<string> l1, List<string> l2)
    {
        if (l1.Count != l2.Count) return false;
        for (int i = 0; i < targetButtonSequence.Count; i++)
        {
            if (l1[i] != l2[i]) return false;
        }
        return true;
    }
}

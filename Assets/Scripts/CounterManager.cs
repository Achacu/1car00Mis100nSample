using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManager : Singleton<CounterManager>
{
    public static event Action<uint> OnCounterChanged = delegate { };
    public static event Action<uint> OnCounterStarted = delegate { };
    public static event Action<uint> OnCounterFinished = delegate { };

    [SerializeField, ContextMenuItem("GoTo90","GoTo90")]
    private float counterTimeStep = 1;
    Coroutine runningCoroutine = null;

    uint currentCount = 0;
    uint totalCount = 100;

    public void GoTo90() => currentCount = 90;

    public void StartCounter()
    {
        if (runningCoroutine == null)
        {
            OnCounterStarted(currentCount);
            runningCoroutine = StartCoroutine(StartCounting());
        }
    }
    public void StopCounter()
    {
        if (runningCoroutine != null) StopCoroutine(runningCoroutine);
    }

    private IEnumerator StartCounting()
    {
        OnCounterChanged(currentCount);
        while (currentCount < totalCount)
        {
            yield return new WaitForSeconds(counterTimeStep);
            currentCount++;
            OnCounterChanged(currentCount);
        }
        OnCounterFinished(totalCount);
    }
}

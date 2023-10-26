using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasLeakManager : MonoBehaviour
{
    private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void OnEnable()
    {
        FlagManager.OnFlagChange += CheckGasFlag;
    }
    public void OnDisable()
    {
        FlagManager.OnFlagChange -= CheckGasFlag;
    }

    private void CheckGasFlag(FlagName flag, bool value)
    {
        if (flag != FlagName.PipeValvePuzzleDone) return;
        gameObject.SetActive(false);
    }
}

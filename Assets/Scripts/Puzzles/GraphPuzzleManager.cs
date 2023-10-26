using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphPuzzleManager : MonoBehaviour
{
    [SerializeField] private SliderGrabbableInteractable amplitudeSlider;
    [SerializeField] private SliderGrabbableInteractable frequencySlider;
    [SerializeField] private SliderGrabbableInteractable phaseSlider;

    [SerializeField] private GraphDrawer adjustableGraph;
    [SerializeField] private GraphDrawer referenceGraph;

    [SerializeField] private float maxFreq = 10f;
    [SerializeField] private float maxPhase = 3.1416f;
    // Start is called before the first frame update
    void Start()
    {
        A = refA;
        w = refW;
        phase = refPhase;
        UpdateGraph(referenceGraph);
    }

    public void OnEnable()
    {
        amplitudeSlider.OnUpdateValue += UpdateAmplitude;
        frequencySlider.OnUpdateValue += UpdateFrequency;
        phaseSlider.OnUpdateValue += UpdatePhase;
    }
    public void OnDisable()
    {
        amplitudeSlider.OnUpdateValue -= UpdateAmplitude;
        frequencySlider.OnUpdateValue -= UpdateFrequency;
        phaseSlider.OnUpdateValue -= UpdatePhase;
    }
    [SerializeField] private float A = 1f;
    [SerializeField] private float w = 0f;
    [SerializeField] private float phase = 0;
    [SerializeField] private float refA = 1f;
    [SerializeField] private float refW = 0f;
    [SerializeField] private float refPhase = 0;
    private void UpdateAmplitude(float value)
    {
        //print("update amplitude: " + value);
        A = value;
        UpdateGraph(adjustableGraph);
    }
    private void UpdateFrequency(float value)
    {
        w = value * maxFreq;
        UpdateGraph(adjustableGraph);
    }
    private void UpdatePhase(float value)
    {
        phase = value * maxPhase;
        UpdateGraph(adjustableGraph);
    }
    [SerializeField] private float amplitudeErrorMargin = 0.1f;
    [SerializeField] private float frequencyErrorMargin = 1f;
    [SerializeField] private float phaseErrorMargin = 0.1f;
    private bool puzzleSolved = false;
    private void UpdateGraph(GraphDrawer graph)
    {
        if (puzzleSolved) return;

        graph.GraphFunction((float x) => (A * Mathf.Sin(w * x + phase)));

        if (Time.timeSinceLevelLoad < 0.5f) return; 

        if((Mathf.Abs(A - refA) < amplitudeErrorMargin) && (Mathf.Abs(w - refW) < frequencyErrorMargin) 
            && (Mathf.Abs(phase - refPhase) < phaseErrorMargin))
        {
            A = refA;
            w = refW;
            phase = refPhase;
            graph.GraphFunction((float x) => (A * Mathf.Sin(w * x + phase)));

            puzzleSolved = true;
            FlagManager.Instance.SetFlag(FlagName.WaveSliderPuzzleDone, true);
        }
    }
}

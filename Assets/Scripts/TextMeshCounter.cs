using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshCounter : MonoBehaviour
{
    TextMeshPro textMesh = null;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    void Start()
    {
        CounterManager.OnCounterChanged += OnCounterChanged;
    }

    void OnCounterChanged(uint newValue)
    {
        textMesh.SetText(newValue.ToString());
    }

    public void OnEnable()
    {
        CounterManager.OnCounterChanged += OnCounterChanged;
    }
    public void OnDisable()
    {
        CounterManager.OnCounterChanged -= OnCounterChanged;
    }
}

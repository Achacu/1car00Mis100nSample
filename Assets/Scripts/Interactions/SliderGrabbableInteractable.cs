using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SliderGrabbableInteractable : ConstrainedGrabbableInteractable
{
    public event System.Action<float> OnUpdateValue = delegate { };

    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;

    private float limitDst;
    public override void Awake()
    {
        base.Awake();
        limitDst = (rightLimit.position - leftLimit.position).magnitude;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        //clamp between limits
        float normalizedDst = (rb.position - leftLimit.position).magnitude / limitDst;
        rb.position = Vector3.Lerp(leftLimit.position, rightLimit.position, normalizedDst);

        OnUpdateValue.Invoke(normalizedDst);
    }
}

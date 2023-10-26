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
        limitDst = (rightLimit.position.z - leftLimit.position.z);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector3 pos = rb.position; 
        pos.z = Mathf.Clamp(pos.z, leftLimit.position.z, rightLimit.position.z);
        rb.position = pos;

        OnUpdateValue.Invoke((transform.position.z - leftLimit.position.z) / limitDst);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonAnim : MonoBehaviour
{
    [SerializeField] private EventSender trigger;
    [SerializeField] private float pressTime = 0.5f;
    [SerializeField] private float pressDst = 0.1f;
    private Coroutine animCorot;
    public void OnValidate()
    {
        if (!trigger) trigger = GetComponent<EventSender>();
    }
    public void OnEnable()
    {
        if (trigger) trigger.OnActivate += StartPressButtonAnim;
    }    
    public void OnDisable()
    {
        if (trigger) trigger.OnActivate -= StartPressButtonAnim;
    }

    private void StartPressButtonAnim(EventSender obj)
    {
        if(animCorot != null)
        {
            StopCoroutine(animCorot);
            transform.position -= transform.forward * pressDst; //button release
        }
        animCorot = StartCoroutine(PressButtonCorot());
    }

    private IEnumerator PressButtonCorot()
    {
        transform.position += transform.forward * pressDst; //button press
        yield return new WaitForSeconds(pressTime);
        transform.position -= transform.forward * pressDst; //button release
        animCorot = null;
    }
}

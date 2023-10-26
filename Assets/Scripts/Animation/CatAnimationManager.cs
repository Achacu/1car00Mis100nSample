using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimationManager : Singleton<CatAnimationManager>
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform playerT;
    public void SetScared(bool value)
    {
        anim.SetBool("isScared", value);
    }
    public void SetSitting(bool value)
    {
        anim.SetBool("isSitting", value);
    }
    public void SetSleeping(bool value)
    {
        anim.SetBool("isSleeping", value);
    }
    public void SetStretchingTrigger()
    {
        anim.SetTrigger("stretchTrigger");
    }
    public void SetWarningTrigger()
    {
        anim.SetTrigger("warningTrigger");
    }
    public void SetHappyTrigger()
    {
        anim.SetTrigger("happyTrigger");
    }

    public void Update()
    {
        transform.rotation = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(playerT.position - transform.position, transform.up), transform.up);
    }
}

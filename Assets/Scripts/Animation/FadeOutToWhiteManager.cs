using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutToWhiteManager : Singleton<FadeOutToWhiteManager>
{
    Animator animator;

    public void StartAnimation() {
        animator = GetComponent<Animator>();
        animator.enabled = true;
    }
}

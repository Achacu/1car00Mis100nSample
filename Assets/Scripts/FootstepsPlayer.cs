using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsPlayer : MonoBehaviour
{
    [SerializeField] private CharMove move;
    [SerializeField] private FMODUnity.EventReference footstepsRef;

    public void Awake()
    {
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        move.OnMoveChange += ChangeFootstepsState;
        SceneManager.OnGameplaySceneLoad += CreateFootstepsInstance;
    }
    void OnDisable()
    {
        move.OnMoveChange -= ChangeFootstepsState;
        SceneManager.OnGameplaySceneLoad -= CreateFootstepsInstance;
        ChangeFootstepsState(false);
    }

    private FMOD.Studio.EventInstance footsteps;
    private void ChangeFootstepsState(bool moving)
    {
        if (moving)
        {
            footsteps.start();
        }
        else
        {
            footsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    // Update is called once per frame
    private void CreateFootstepsInstance()
    {
        //if (FMODUnity.RuntimeManager.AnySampleDataLoading()) return;
        //if (!isDataInitialized)
        //{
        footsteps = FMODUnity.RuntimeManager.CreateInstance(footstepsRef);
       //     isDataInitialized = true;
        //}
    }
}

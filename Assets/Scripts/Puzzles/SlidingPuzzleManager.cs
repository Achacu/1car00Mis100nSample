using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzleManager : MonoBehaviour
{
    [SerializeField] private SlidingPuzzleGrabbableInteractable[] slidingCubes;
    [SerializeField] private float cubeSize = 0.15f;

    public void OnValidate()
    {
        if(slidingCubes.Length == 0) slidingCubes = GetComponentsInChildren<SlidingPuzzleGrabbableInteractable>();
    }
    // Start is called before the first frame update
    void Awake()
    {
        OnValidate();
    }

    public void OnEnable()
    {
        foreach (var cube in slidingCubes) cube.OnRelease += CheckPuzzle;
    }
    public void OnDisable()
    {
        foreach (var cube in slidingCubes) cube.OnRelease -= CheckPuzzle;
    }
    //bool solvedPuzzle;
    private void CheckPuzzle()
    {
        //print("check puzzle");

        Vector2 gridPos;
        Vector3 toCube;
        Transform cubeT;
        bool failedPuzzle = false;
        for(int i = 0; i < slidingCubes.Length; i++)
        {
            cubeT = slidingCubes[i].transform;
            toCube = (cubeT.position - transform.position);
            gridPos.x = Mathf.Round(Vector3.ProjectOnPlane(toCube, transform.up).magnitude/cubeSize);            
            gridPos.y = Mathf.Round(toCube.y/cubeSize);
            //print(gridPos);

            cubeT.position = transform.position + transform.TransformVector(gridPos * cubeSize); //snaps cubes in grid
            if ((gridPos.x + 3 * gridPos.y) != i) failedPuzzle = true;
        }
        if(!failedPuzzle)
        {
            //solvedPuzzle = true;
            FlagManager.Instance.SetFlag(FlagName.SlidingPuzzleDone, true);
            print("sliding puzzle solved!");
        }
    }
}

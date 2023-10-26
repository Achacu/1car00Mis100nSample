using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum FlagName
{
    NullFlag,
    ButtonMasherPuzzleDone,
    ColoredButtonPuzzleDone,
    KeypadPuzzleDone,
    WaveSliderPuzzleDone,
    PipeValvePuzzleDone,
    HardDrivePuzzleDone,
    CablePuzzleDone,
    PhotoAlbumPuzzleDone,
    XylophonePuzzleDone,
    SlidingPuzzleDone,
    SlotToyPuzzleDone,
    AbacusPuzzleDone,
}


[Serializable]
public class FlagState
{
    [SerializeField] public FlagName key;
    [SerializeField] public bool value;

    public FlagState(FlagName key = FlagName.NullFlag, bool value = false)
    {
        this.key = key;
        this.value = value;
    }
}

public class FlagManager : Singleton<FlagManager>
{
    public static event Action<FlagName, bool> OnFlagChange = delegate { };

    [SerializeField, ContextMenuItem("SetChildPuzzleFlags", "SetChildPuzzleFlags")]
    private uint timesPlayed = 0;
    [SerializeField]
    private List<FlagState> flags;

    public void ResetFlags()
    {
        this.flags = new List<FlagState>();
        foreach (FlagName flagName in Enum.GetValues(typeof(FlagName)))
        {
            this.flags.Add(new FlagState(flagName));
        }
    }

    public void SetChildPuzzleFlags()
    {
        SetFlag(FlagName.AbacusPuzzleDone, true);
        SetFlag(FlagName.XylophonePuzzleDone, true);
        SetFlag(FlagName.SlotToyPuzzleDone, true);
        SetFlag(FlagName.SlidingPuzzleDone, true);
        SetFlag(FlagName.PhotoAlbumPuzzleDone, true);
    }
    //void OnValidate()
    //{
    //    ResetFlags();
    //}

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        //OnValidate();
        ResetFlags();
    }

    // Changes a flag and returns if it has indeed changed.
    public bool SetFlag(FlagName key, bool value)
    {
        int foundIndex = this.flags.FindIndex(item => item.key.Equals(key));

        // No flag found, did not change
        if (foundIndex < 0) return false;

        // Flag found, did not change
        bool didChange = this.flags[foundIndex].value != value;
        if (!didChange) return false;

        // Flag found, did change
        this.flags[foundIndex].value = value;
        OnFlagChange(key, value);
        return true;
    }

    public bool GetFlag(FlagName key)
    {
        int foundIndex = this.flags.FindIndex(item => item.key.Equals(key));
        if (foundIndex < 0) return false;
        return this.flags[foundIndex].value;
    }

    public void IncreaseTimesPlayed()
    {
        timesPlayed++;
    }
}

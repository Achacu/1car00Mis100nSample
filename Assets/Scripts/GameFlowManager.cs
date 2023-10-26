using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : Singleton<GameFlowManager>
{
    [System.Serializable]
    public class ChildPuzzleData
    {
        public FlagName flag;

        public FMODUnity.EventReference clueReference;
        public FMOD.Studio.EventInstance clueInstance;
        public SubtitleDisplayer.SubtitleList clueSub;

        public FMODUnity.EventReference feedbackRef;
        public FMOD.Studio.EventInstance feedbackInstance;
        public SubtitleDisplayer.SubtitleList feedbackSub;

        public Transform confettiCenter;
    }
    [SerializeField] private List<ChildPuzzleData> childPuzzleData;

    private bool isDataInitialized = false;

    [SerializeField] private GameObject needToClickText;

    [SerializeField] private Animator fadeInFromBlack;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform catTransform;

    [SerializeField] private GameObject confettiPrefab;

    // References
    [SerializeField] private FMODUnity.EventReference backgroundMusicReference;
    [SerializeField] private FMODUnity.EventReference secondMeteoriteImpactReference;
    [SerializeField] private FMODUnity.EventReference goodEndingReference;
    [SerializeField] private FMODUnity.EventReference badEndingReference;
    [SerializeField] private FMODUnity.EventReference papaVenReference;

    // Cat dialog References
    [SerializeField] private FMODUnity.EventReference catastrophicFailureReference;
    [SerializeField] private FMODUnity.EventReference purgeSuggestionReference;
    [SerializeField] private FMODUnity.EventReference malfunctioningReference;
    [SerializeField] private FMODUnity.EventReference newBodyDetectedReference;
    [SerializeField] private FMODUnity.EventReference newMeteoriteImpactReference;
    [SerializeField] private FMODUnity.EventReference meteoriteChangeReference;
    [SerializeField] private FMODUnity.EventReference overloadCoolingReference;
    [SerializeField] private FMODUnity.EventReference stableCoolingReference;

    //Feedback references
    [SerializeField] private FMODUnity.EventReference seriousPuzzleCorrectRef;
    [SerializeField] private FMODUnity.EventReference childPuzzleCorrectRef;

    //// Kid clues references
    //[SerializeField] private FMODUnity.EventReference clue1Reference;
    //[SerializeField] private FMODUnity.EventReference clue2Reference;
    //[SerializeField] private FMODUnity.EventReference clue3Reference;
    //[SerializeField] private FMODUnity.EventReference clue4Reference;
    //[SerializeField] private FMODUnity.EventReference clue5Reference;

    // Instances
    private FMOD.Studio.EventInstance backgroundMusic;
    private FMOD.Studio.EventInstance secondMeteoriteImpact;
    private FMOD.Studio.EventInstance goodEnding;
    private FMOD.Studio.EventInstance badEnding;
    private FMOD.Studio.EventInstance papaVen;

    // Cat dialog instances
    private FMOD.Studio.EventInstance catastrophicFailure;
    private FMOD.Studio.EventInstance purgeSuggestion;
    private FMOD.Studio.EventInstance malfunctioning;
    private FMOD.Studio.EventInstance newBodyDetected;
    private FMOD.Studio.EventInstance newMeteoriteImpact;
    private FMOD.Studio.EventInstance meteoriteChange;
    private FMOD.Studio.EventInstance overloadCooling;
    private FMOD.Studio.EventInstance stableCooling;

    //Feedback instances
    private FMOD.Studio.EventInstance seriousPuzzleCorrect;
    private FMOD.Studio.EventInstance childPuzzleCorrect;

    //// Kid clues instances
    //private FMOD.Studio.EventInstance clue1;
    //private FMOD.Studio.EventInstance clue2;
    //private FMOD.Studio.EventInstance clue3;
    //private FMOD.Studio.EventInstance clue4;
    //private FMOD.Studio.EventInstance clue5;
    //private List<FMOD.Studio.EventInstance> clues;
    //private List<SubtitleDisplayer.SubtitleList> clueSubtitles;


    public override void Awake()
    {
        base.Awake();
    }

    void GameFlowStart()
    {
        isDataInitialized = true;
        //FlagManager.Instance.ResetFlags();
        print("start");

        backgroundMusic = FMODUnity.RuntimeManager.CreateInstance(backgroundMusicReference);
        secondMeteoriteImpact = FMODUnity.RuntimeManager.CreateInstance(secondMeteoriteImpactReference);
        goodEnding = FMODUnity.RuntimeManager.CreateInstance(goodEndingReference);
        badEnding = FMODUnity.RuntimeManager.CreateInstance(badEndingReference);
        papaVen = FMODUnity.RuntimeManager.CreateInstance(papaVenReference);

        // Cat dialog
        catastrophicFailure = FMODUnity.RuntimeManager.CreateInstance(catastrophicFailureReference);
        purgeSuggestion = FMODUnity.RuntimeManager.CreateInstance(purgeSuggestionReference);
        malfunctioning = FMODUnity.RuntimeManager.CreateInstance(malfunctioningReference);
        newBodyDetected = FMODUnity.RuntimeManager.CreateInstance(newBodyDetectedReference);
        newMeteoriteImpact = FMODUnity.RuntimeManager.CreateInstance(newMeteoriteImpactReference);
        meteoriteChange = FMODUnity.RuntimeManager.CreateInstance(meteoriteChangeReference);
        overloadCooling = FMODUnity.RuntimeManager.CreateInstance(overloadCoolingReference);
        stableCooling = FMODUnity.RuntimeManager.CreateInstance(stableCoolingReference);

        //Feedback
        childPuzzleCorrect = FMODUnity.RuntimeManager.CreateInstance(childPuzzleCorrectRef);
        seriousPuzzleCorrect = FMODUnity.RuntimeManager.CreateInstance(seriousPuzzleCorrectRef);

        //// Kid clues
        //clue1 = FMODUnity.RuntimeManager.CreateInstance(clue1Reference);
        //clue2 = FMODUnity.RuntimeManager.CreateInstance(clue2Reference);
        //clue3 = FMODUnity.RuntimeManager.CreateInstance(clue3Reference);
        //clue4 = FMODUnity.RuntimeManager.CreateInstance(clue4Reference);
        //clue5 = FMODUnity.RuntimeManager.CreateInstance(clue5Reference);
        //clues = new List<FMOD.Studio.EventInstance>() { clue1, clue2, clue3, clue4, clue5 };
        //clueSubtitles = new List<SubtitleDisplayer.SubtitleList>() { SubtitleDisplayer.SubtitleList.Blobbyfu, SubtitleDisplayer.SubtitleList.Abaco 
        //    , SubtitleDisplayer.SubtitleList.Fotos, SubtitleDisplayer.SubtitleList.Xilofono, SubtitleDisplayer.SubtitleList.Cohete };
        ChildPuzzleData data;
        for(int i = 0; i < childPuzzleData.Count; i++)
        {
            data = childPuzzleData[i];
            data.clueInstance = FMODUnity.RuntimeManager.CreateInstance(data.clueReference);
            data.feedbackInstance = FMODUnity.RuntimeManager.CreateInstance(data.feedbackRef);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(data.feedbackInstance, catTransform);
        }

        backgroundMusic.start();
        fadeInFromBlack.enabled = true;                    
    }

    void OnCounterFinished(uint counterValue)
    {
        FlagManager.Instance.IncreaseTimesPlayed();
        backgroundMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        if (
            FlagManager.Instance.GetFlag(FlagName.PhotoAlbumPuzzleDone) &&
            FlagManager.Instance.GetFlag(FlagName.XylophonePuzzleDone) &&
            FlagManager.Instance.GetFlag(FlagName.SlidingPuzzleDone) &&
            FlagManager.Instance.GetFlag(FlagName.SlotToyPuzzleDone) &&
            FlagManager.Instance.GetFlag(FlagName.AbacusPuzzleDone)
        )
        {
            SceneManager.Instance.GoToEnding();
        }
        else
        {
            SceneManager.Instance.GoToGameplay();
        }
        FlagManager.Instance.ResetFlags();
    }
    private void DisplaySubtitle(SubtitleDisplayer.SubtitleList sub) => SubtitleDisplayer.Instance.SetLineGroup(sub);
    void OnCounterChanged(uint counterValue)
    {
        int randomClueIndex = 0;
        switch (counterValue)
        {
            case 0:
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(catastrophicFailure, catTransform);
                catastrophicFailure.start();
                DisplaySubtitle(SubtitleDisplayer.SubtitleList.Segundos100);
                CatAnimationManager.Instance.SetWarningTrigger();
                CatAnimationManager.Instance.SetScared(true);
                break;
            case 4:
                CatAnimationManager.Instance.SetScared(false);
                break;
            case 15:
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(purgeSuggestion, catTransform);
                purgeSuggestion.start();
                DisplaySubtitle(SubtitleDisplayer.SubtitleList.PurgarPurticulas);
                CatAnimationManager.Instance.SetWarningTrigger();
                break;
            case 20:
                randomClueIndex = RandomClue();

                break;
            case 30:
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(newBodyDetected, catTransform);
                newBodyDetected.start();
                DisplaySubtitle(SubtitleDisplayer.SubtitleList.CuerpoDetectado);
                CatAnimationManager.Instance.SetWarningTrigger();
                break;
            case 43:
                bool atLeastOneChildPuzzle = FlagManager.Instance.GetFlag(FlagName.PhotoAlbumPuzzleDone) || FlagManager.Instance.GetFlag(FlagName.XylophonePuzzleDone) || FlagManager.Instance.GetFlag(FlagName.SlidingPuzzleDone) || FlagManager.Instance.GetFlag(FlagName.SlotToyPuzzleDone) || FlagManager.Instance.GetFlag(FlagName.AbacusPuzzleDone);
                if (atLeastOneChildPuzzle)
                {
                    // At least one child puzzle completed
                    FMODUnity.RuntimeManager.AttachInstanceToGameObject(meteoriteChange, catTransform);
                    meteoriteChange.start();
                    DisplaySubtitle(SubtitleDisplayer.SubtitleList.CambiandoRumbo);
                    CatAnimationManager.Instance.SetHappyTrigger();
                }
                else
                {
                    FMODUnity.RuntimeManager.AttachInstanceToGameObject(newMeteoriteImpact, catTransform);
                    newMeteoriteImpact.start();
                    secondMeteoriteImpact.start();
                    DisplaySubtitle(SubtitleDisplayer.SubtitleList.CatAlizadorApagado);
                    CatAnimationManager.Instance.SetScared(true);
                }
                break;
            case 45:
                CatAnimationManager.Instance.SetScared(false);
                break;
            case 55:
                randomClueIndex = RandomClue();
                break;
            case 65:
                bool atLeast1ChildPuzzle = FlagManager.Instance.GetFlag(FlagName.PhotoAlbumPuzzleDone) || FlagManager.Instance.GetFlag(FlagName.XylophonePuzzleDone) || FlagManager.Instance.GetFlag(FlagName.SlidingPuzzleDone) || FlagManager.Instance.GetFlag(FlagName.SlotToyPuzzleDone) || FlagManager.Instance.GetFlag(FlagName.AbacusPuzzleDone);
                if (!atLeast1ChildPuzzle)
                {
                    FMODUnity.RuntimeManager.AttachInstanceToGameObject(malfunctioning, catTransform);
                    malfunctioning.start();
                    DisplaySubtitle(SubtitleDisplayer.SubtitleList.MiaulfuncionaPulsores);
                    CatAnimationManager.Instance.SetWarningTrigger();
                }
                break;
            case 75:
                bool atLeastThreeChildPuzzles = ((FlagManager.Instance.GetFlag(FlagName.PhotoAlbumPuzzleDone) ? 1 : 0) +
                 (FlagManager.Instance.GetFlag(FlagName.XylophonePuzzleDone) ? 1 : 0) +
                 (FlagManager.Instance.GetFlag(FlagName.SlidingPuzzleDone) ? 1 : 0) +
                 (FlagManager.Instance.GetFlag(FlagName.SlotToyPuzzleDone) ? 1 : 0) +
                 (FlagManager.Instance.GetFlag(FlagName.AbacusPuzzleDone) ? 1 : 0)) >= 3;

                if (atLeastThreeChildPuzzles)
                {
                    // At least three child puzzles completed
                    FMODUnity.RuntimeManager.AttachInstanceToGameObject(stableCooling, catTransform);
                    stableCooling.start();
                    DisplaySubtitle(SubtitleDisplayer.SubtitleList.RefrigeracionEstable);
                    CatAnimationManager.Instance.SetHappyTrigger();
                }
                else
                {
                    FMODUnity.RuntimeManager.AttachInstanceToGameObject(overloadCooling, catTransform);
                    overloadCooling.start();
                    DisplaySubtitle(SubtitleDisplayer.SubtitleList.SobrecargaRefrigeracion);
                    CatAnimationManager.Instance.SetScared(true);
                }
                break;
            case 77:
                CatAnimationManager.Instance.SetScared(false);
                break;
            case 85:
                randomClueIndex = RandomClue();
                break;
            case 95:
                if (
                    FlagManager.Instance.GetFlag(FlagName.PhotoAlbumPuzzleDone) &&
                    FlagManager.Instance.GetFlag(FlagName.XylophonePuzzleDone) &&
                    FlagManager.Instance.GetFlag(FlagName.SlidingPuzzleDone) &&
                    FlagManager.Instance.GetFlag(FlagName.SlotToyPuzzleDone) &&
                    FlagManager.Instance.GetFlag(FlagName.AbacusPuzzleDone)
                )
                {
                    // Good ending
                    //PreGoodEnding();
                }
                else
                {
                    // Bad ending                    
                    badEnding.start();
                    CatAnimationManager.Instance.SetScared(true);
                }
                break;
            case 97:
                FadeOutToWhiteManager.Instance.StartAnimation();
                break;
            default:
                break;
        }
    }

    private void GoodEnding()
    {
        PreGoodEnding();

        CounterManager.Instance.StopCounter();
        StartCoroutine(GoodEndingTiming());

    }
    private IEnumerator GoodEndingTiming()
    {
        yield return new WaitForSeconds(4);
        FadeOutToWhiteManager.Instance.StartAnimation();
        yield return new WaitForSeconds(5);
        backgroundMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        backgroundMusic.release();
        OnCounterFinished(100); //ends game
    }

    private void PreGoodEnding()
    {
        //DontDestroyOnLoad(this.gameObject);
        goodEnding.start();
        goodEnding.release();
        papaVen.start();
        papaVen.release();
        DisplaySubtitle(SubtitleDisplayer.SubtitleList.Papa);
        CatAnimationManager.Instance.SetHappyTrigger();
    }

    private int RandomClue()
    {
        if (childPuzzleData.Count == 0) //all puzzles completed
            return -1;

        int randomClueIndex = Random.Range(0, childPuzzleData.Count);
        childPuzzleData[randomClueIndex].clueInstance.start();
        DisplaySubtitle(childPuzzleData[randomClueIndex].clueSub);

        CatAnimationManager.Instance.SetStretchingTrigger();
        return randomClueIndex;
    }

    public void StartGame()
    {
        CounterManager.Instance.StartCounter();
    }

    public void PostStartClick()
    {
        return;
        // MEGA HACK
        Debug.Log("HEY");
        needToClickText.SetActive(false);
        fadeInFromBlack.enabled = true;
        GameFlowStart();
    }

    private void PressedInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(!isDataInitialized) PostStartClick();
    }

    public void OnEnable()
    {
        FlagManager.OnFlagChange += ProcessFlag;
        CounterManager.OnCounterFinished += OnCounterFinished;
        CounterManager.OnCounterChanged += OnCounterChanged;
        playerInput.Controls.General.Interact.performed += PressedInteract;
        playerInput.Controls.General.Interact.performed += PressedInteract;

        SceneManager.OnGameplaySceneLoad += GameFlowStart;
    }

    public void OnDisable()
    {
        FlagManager.OnFlagChange -= ProcessFlag;
        CounterManager.OnCounterFinished -= OnCounterFinished;
        CounterManager.OnCounterChanged -= OnCounterChanged;
        playerInput.Controls.General.Interact.performed -= PressedInteract;

        SceneManager.OnGameplaySceneLoad -= GameFlowStart;
    }
    private void ProcessFlag(FlagName flag, bool state)
    {
        if (!state) return;

        ChildPuzzleData data;
        for(int i = 0; i < childPuzzleData.Count; i++)
        {
            data = childPuzzleData[i];
            if(data.flag == flag)
            {
                childPuzzleData.Remove(data); //useful for not playing random clue of already completed puzzles
                if (childPuzzleData.Count == 0)
                {
                    GoodEnding(); //When all child puzzles are solved
                    return;
                } 

                data.feedbackInstance.start();
                DisplaySubtitle(data.feedbackSub);
                Instantiate(confettiPrefab, data.confettiCenter.position, Quaternion.identity);
                break;
            }
        }

        switch(flag)
        {
            case FlagName.NullFlag:
                break;
            case FlagName.ButtonMasherPuzzleDone:
            case FlagName.ColoredButtonPuzzleDone:
            case FlagName.KeypadPuzzleDone:
            case FlagName.WaveSliderPuzzleDone:
            case FlagName.PipeValvePuzzleDone:
            case FlagName.HardDrivePuzzleDone:
            case FlagName.CablePuzzleDone:
                seriousPuzzleCorrect.start();
                print("serious puzzle solved");
                break;
            case FlagName.PhotoAlbumPuzzleDone:
            case FlagName.XylophonePuzzleDone:
            case FlagName.SlidingPuzzleDone:
            case FlagName.SlotToyPuzzleDone:
            case FlagName.AbacusPuzzleDone:
                childPuzzleCorrect.start();
                CatAnimationManager.Instance.SetHappyTrigger();
                print("child puzzle solved");
                break;
            default:
                break;
        }

        //if (childPuzzleData.Count == 0) GoodEnding(); //When all child puzzles are solved
    }
}

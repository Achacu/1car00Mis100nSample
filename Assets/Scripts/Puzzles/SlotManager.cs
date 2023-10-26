using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{

    //[Serializable]
    //class SlotCorrectPair
    //{
    //    public ObjectSlotInteractable slot;
    //    public bool isCorrect;

    //    public SlotCorrectPair(ObjectSlotInteractable slots, bool areSlotsCorrect)
    //    {
    //        this.slot = slots;
    //        this.isCorrect = areSlotsCorrect;
    //    }
    //}

    [SerializeField] private ObjectSlotInteractable[] slots;
    private Dictionary<ObjectSlotInteractable, bool> slotCorrectPairs = new Dictionary<ObjectSlotInteractable, bool>();
    [SerializeField] private bool allSlotsCorrect;
    [SerializeField] private FlagName myFlag;

    private void OnValidate()
    {
        slots = GetComponentsInChildren<ObjectSlotInteractable>();
    }
    private void Awake()
    {
        //Initialize dictionary
        foreach (var slot in slots) slotCorrectPairs[slot] = false;
    }

    private void OnEnable()
    {
        foreach(var slot in slots)
        {
            slot.OnSlotCorrectChange += UpdateSlotCorrectState;
        }
    }
    private void OnDisable()
    {
        foreach (var slot in slots)
        {
            slot.OnSlotCorrectChange -= UpdateSlotCorrectState;
        }
    }

    private void UpdateSlotCorrectState(ObjectSlotInteractable slot, bool isCorrect)
    {
        if (allSlotsCorrect) return;
        slotCorrectPairs[slot] = isCorrect;
        //print(slot.gameObject.name + " " + slotCorrectPairs[slot]);
        CheckIfAllCorrect();
    }

    private void CheckIfAllCorrect()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slotCorrectPairs[slots[i]] == false) return;
        }
        print("all correct!");
        allSlotsCorrect = true; 
        FlagManager.Instance.SetFlag(myFlag, true);
    }


    // Update is called once per frame
    void Update()
    {        
    }          
}

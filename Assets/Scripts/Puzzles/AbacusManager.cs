using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbacusManager : MonoBehaviour
{
    public List<Collider> collidingBalls = new List<Collider>();
    [SerializeField] private int numOfBallsNeededOnRight = 3;
    [SerializeField] private float makeSureTime = 1f;
    private bool puzzleSolved = false;
    public void OnTriggerEnter(Collider other)
    {
        if (puzzleSolved) return;
        ConstrainedGrabbableInteractable grabbable = other.GetComponent<ConstrainedGrabbableInteractable>();
        if (!grabbable) return;

        if (grabbable.slotType == ObjectSlotInteractable.SlotType.AbacusBall)
            collidingBalls.Add(other);

        PuzzleSolvedCheck();
    }
    public void OnTriggerExit(Collider other)
    {
        if (puzzleSolved) return;
        if (collidingBalls.Contains(other)) collidingBalls.Remove(other);
        PuzzleSolvedCheck();
    }

    private void PuzzleSolvedCheck()
    {
        if (collidingBalls.Count == numOfBallsNeededOnRight)
        {
            if (waitCorot != null)
            {
                StopCoroutine(waitCorot);
            }
            waitCorot = StartCoroutine(WaitToMakeSureOnly3BallsAreIn());
        }
    }

    private Coroutine waitCorot;
    private IEnumerator WaitToMakeSureOnly3BallsAreIn()
    {
        yield return new WaitForSeconds(makeSureTime);
        if (collidingBalls.Count != numOfBallsNeededOnRight) yield break;

        puzzleSolved = true;
        FlagManager.Instance.SetFlag(FlagName.AbacusPuzzleDone, true);
        print("abacus solved!");
    }
}

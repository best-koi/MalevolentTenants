using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyPuzzle puzzle;

    [SerializeField] private string text = "(E) Open Door";
    public string interactionText => text;

    public bool Interact(Interactor interactor)
    {
        if (puzzle.isSolved)
        {
            Debug.LogFormat("DoorManager.Interact Door opened");
        }
        else
        {
            Debug.LogFormat("DoorManager.Interact Door is still locked");
        }

        return puzzle.isSolved;
    }
}

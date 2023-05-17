using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string text = "(E) Pushing ";
    public string interactionText=>text;
    public bool Interact(Interactor interactor){
        Debug.Log("Pushing button!");
        return true;
    }
}

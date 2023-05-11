using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string text = "(E) Open Door";
    public string interactionText =>text;
    public bool Interact(Interactor interactor){
        var inventory=interactor.GetComponent<TestInventory>();
        if(inventory==null) return false;
        if(inventory.HasKey){
            Debug.Log("Opening Door!");
            return true;
        }
        Debug.Log("You need a key to open the door.");
        return false;
    }
        
}

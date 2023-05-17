using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItems : MonoBehaviour, IInteractable
{
    [SerializeField] private string text ="(E) Pick Up";
    public string interactionText =>text;
    public bool Interact(Interactor interactor){
        
        Debug.Log("Picking up!");
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadOnlyItem : MonoBehaviour, IInteractable
{
   [SerializeField] private string text ="(E) to Read";
    public string interactionText =>text;
    private ReadInteraction readnotes;
    public bool Interact(Interactor interactor){
        readnotes=GetComponent<ReadInteraction>();
        if(readnotes!=null){
            readnotes.isReadNote=true;
            return true;   
        }
        return false;
        
    }
}


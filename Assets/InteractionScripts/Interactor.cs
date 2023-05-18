using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius=0.5f;
    [SerializeField] private LayerMask InteractableLayer;
    [SerializeField] private int numFound;
    [SerializeField] private TextInteraction textinteraction;
    private IInteractable interactableItem;
    private ReadInteraction readinteraction;
    private Collider[] colliders=new Collider[3]; //If overlap, only three items can interact
    private void Update() {
        numFound= Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRadius, colliders, InteractableLayer);
        readinteraction = colliders[0].GetComponent<ReadInteraction>();
        if(numFound>0){
            interactableItem = colliders[0].GetComponent<IInteractable>();
            if(interactableItem!=null){
                if(!textinteraction.IsDisplayed) textinteraction.SetUp(interactableItem.interactionText);
                if(Input.GetKeyDown(KeyCode.E))interactableItem.Interact(this);
            }
        }else{
            if(interactableItem!=null) interactableItem=null;
            if(textinteraction.IsDisplayed) textinteraction.closePanel();
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRadius);
    }
}

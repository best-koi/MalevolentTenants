using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DormDoorManager : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData key;

    [SerializeField] private string text = "(E) Open Door";
    public string interactionText => text;

    public bool Interact(Interactor interactor)
    {
        if (PlayerInventory.Instance.FindItems(key).Count > 0)
        {
            Debug.LogFormat("DoorManager.Interact Door opened");

            return true;
        }
        else
        {
            Debug.LogFormat("DoorManager.Interact Door is still locked");

            return false;
        }
    }
}

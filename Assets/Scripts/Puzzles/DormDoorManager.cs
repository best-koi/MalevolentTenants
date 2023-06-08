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
        List<InventoryItem> keys = PlayerInventory.Instance.FindItems(key);

        if (keys.Count > 0)
        {
            Debug.LogFormat("DoorManager.Interact Door opened");

            PlayerInventory.Instance.UseItem(keys[0]);

            return true;
        }
        else
        {
            Debug.LogFormat("DoorManager.Interact Door is still locked");

            return false;
        }
    }
}

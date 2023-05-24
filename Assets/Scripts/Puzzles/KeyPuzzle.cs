using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPuzzle : MonoBehaviour, IInteractable
{
    [SerializeField] protected ItemData requiredKey;

    [SerializeField] protected string text = "(E) Try Puzzle";
    public string interactionText => text;

    [field: SerializeField] public bool isSolved { get; protected set; } = false;

    public bool Interact(Interactor interactor)
    {
        /*
        Stopgap until UI interfaces for interacting with and selecting items from inventory is added
        Instead, could have KeyPuzzle listen to PlayerInventory.Instance.itemUsedEvent, use item selected from UI on selected puzzle, and handle logic there
        */

        if (isSolved) return false;

        List<InventoryItem> compatibleItems = PlayerInventory.Instance.FindItems(requiredKey);

        if (compatibleItems.Count == 0) return false;

        PlayerInventory.Instance.UseItem(compatibleItems[0]);

        OnSolved(interactor, compatibleItems);

        return true;
    }

    public virtual void OnSolved(Interactor interactor, List<InventoryItem> items)
    {
        Debug.LogFormat("KeyPuzzle: Puzzle solved");

        isSolved = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defunct code; use PickableItems instead 
public class ItemPickUp : PersistentObject, IInteractable
{
    [SerializeField] private ItemData itemData;

    [SerializeField] private int initialStack = 1;

    [SerializeField] private string text = "(E) Pick Up";
    
    public string interactionText => text;

    private InventoryItem createdItem;

    private bool isActive = true;

    public bool Interact(Interactor interactor)
    {
        if (!isActive) return false;

        if (createdItem == null)
        {
            createdItem = PlayerInventory.CreateInventoryItem(itemData, initialStack, string.Empty);
        }

        if (PlayerInventory.Instance.AddItem(createdItem))
        {
            isActive = false;

            gameObject.SetActive(isActive);
        }

        return true;
    }

    public override PersistentObjectData Save()
    {
        List<string> data = new List<string>();
        
        data.Add(isActive ? "1" : "0");
        data.Add(createdItem != null ? createdItem.CurrentStack.ToString() : initialStack.ToString());
        data.Add(createdItem != null ? createdItem.InstanceID : string.Empty);

        return new PersistentObjectData(data.ToArray());
    }

    public override void Load(PersistentObjectData POData)
    {
        isActive = POData.data[0] == "1" ? true : false;
        initialStack = int.Parse(POData.data[1]);
        createdItem = InventoryItem.FindItemByID(POData.data[2]);

        gameObject.SetActive(isActive);
    }
}

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
            PlayerInventory.Instance.itemAddedEvent += Added;
        }

        PlayerInventory.Instance.AddItem(createdItem);

        return true;
    }

    protected virtual void Added(InventoryItem item)
    {
        if (item != createdItem) return;

        PlayerInventory.Instance.itemAddedEvent -= Added;

        isActive = false;

        gameObject.SetActive(isActive);
    }

    public override PersistentObjectData Save()
    {
        List<string> data = new List<string>();
        
        data.Add(createdItem != null ? createdItem.CurrentStack.ToString() : string.Empty);
        data.Add(isActive ? "1" : "0");
        data.Add(createdItem != null ? createdItem.InstanceID : string.Empty);

        return new PersistentObjectData(data.ToArray());
    }

    public override void Load(PersistentObjectData POData)
    {
        if (POData.data[0] != string.Empty)
        {
            InventoryItem item = InventoryItem.FindItemByID(POData.data[2]);
            createdItem = item != null ? item : PlayerInventory.CreateInventoryItem(itemData, int.Parse(POData.data[0]), POData.data[2]);
            PlayerInventory.Instance.itemAddedEvent += Added;
        }

        isActive = POData.data[1] == "1" ? true : false;

        gameObject.SetActive(isActive);
    }
}

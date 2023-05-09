using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : PersistentObject, IInteractable
{
    [SerializeField] private ItemData itemData;

    [SerializeField] private int initialStack = 1;

    private InventoryItem createdItem;

    private bool isActive = true;

    public bool Interact()
    {
        if (!isActive) return false;

        if (createdItem == null)
        {
            createdItem = PlayerInventory.CreateInventoryItem(itemData, initialStack, string.Empty);
            createdItem.itemAdded += Added;
        }

        PlayerInventory.Instance.AddItem(createdItem);

        return true;
    }

    protected virtual void Added()
    {
        createdItem.itemAdded -= Added;

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
            createdItem.itemAdded += Added;
        }

        isActive = POData.data[1] == "1" ? true : false;

        gameObject.SetActive(isActive);
    }
}

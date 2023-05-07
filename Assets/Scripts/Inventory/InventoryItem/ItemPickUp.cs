using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : SceneObject, IInteractable
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
            createdItem = PlayerInventory.CreateInventoryItem(itemData, initialStack);
            createdItem.itemAdded += Added;
        }

        PlayerInventory.Instance.AddItem(createdItem);

        return true;
    }

    protected virtual void Added()
    {
        isActive = false;

        gameObject.SetActive(isActive);
    }

    public override SceneObjectData Save()
    {
        List<string> data = new List<string>();
        
        data.Add(createdItem != null ? createdItem.CurrentStack.ToString() : string.Empty);
        data.Add(isActive ? "1" : "0");

        return new SceneObjectData(data.ToArray());
    }

    public override void Load(SceneObjectData SOData)
    {
        if (SOData.data[0] != string.Empty)
        {
            createdItem = PlayerInventory.CreateInventoryItem(itemData, int.Parse(SOData.data[0]));
            createdItem.itemAdded += Added;
        }

        isActive = SOData.data[1] == "1" ? true : false;

        gameObject.SetActive(isActive);
    }
}

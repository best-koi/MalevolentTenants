using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    [field: SerializeField] public List<InventoryItem> inventory { get; private set; }

    [SerializeField] private int maxInventorySpace = 3;

    private InventoryItem equippedItem;

    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);

        Instance = this;

        // DontDestroyOnLoad(this);
    }

    private bool ValidateSpace(int requiredSpace)
    {
        int availableSpace = maxInventorySpace;

        foreach (InventoryItem item in inventory)
        {
            availableSpace -= item.Data.RequiredSpace;
        }

        return availableSpace >= requiredSpace;
    }

    public bool AddItem(InventoryItem newItem)
    {
        if (newItem == null) return false;

        if (inventory.Contains(newItem)) return false;

        if (!ValidateSpace(newItem.Data.RequiredSpace)) return false;

        foreach(InventoryItem item in inventory)
        {
            if (!ValidateItem(newItem)) return DestroyItem(newItem);

            if (item.Data != newItem.Data) continue;

            if (item.CurrentStack + newItem.CurrentStack <= item.Data.MaxStack)
            {
                item.CurrentStack += newItem.CurrentStack;

                return !ValidateItem(newItem) ? DestroyItem(newItem) : true;
            }
            else
            {
                int addedStack = item.Data.MaxStack - item.CurrentStack;
                item.CurrentStack += addedStack;

                newItem.CurrentStack -= addedStack;
            }
        }

        newItem.OnAdded();
        inventory.Add(newItem);

        return true;
    }

    public bool RemoveItem(InventoryItem removedItem)
    {
        if (removedItem == null) return false;

        if (!inventory.Contains(removedItem)) return false;

        removedItem.OnRemoved();
        inventory.Remove(removedItem);

        return true;
    }

    public GameObject CombineItems(InventoryItem item, InventoryItem otherItem)
    {
        if (item == null || otherItem == null) return null;

        if (!inventory.Contains(item)) return null;
        
        if (!inventory.Contains(otherItem)) return null;

        GameObject combinedResult = item.CombineWith(otherItem);

        if (!ValidateItem(item)) DestroyItem(item);
        if (!ValidateItem(otherItem)) DestroyItem(otherItem);

        return combinedResult;
    }

    public bool Clear()
    {
        inventory.Clear();

        return true;
    }

    public bool UseEquipped(GameObject[] others = null)
    {
        return UseItem(equippedItem, others);
    }

    public bool UseItem(InventoryItem item, GameObject[] others = null)
    {
        if (item == null) return true;

        if (!inventory.Contains(item)) return false;

        bool result = item.Use(others);

        if (!ValidateItem(item)) DestroyItem(item);

        return result;
    }

    public bool EquipItem(InventoryItem item)
    {
        if (item == null)
        {
            equippedItem = null;
            return true;
        }

        if (!inventory.Contains(item)) return false;

        if (!item.Data.Equippable) return false;

        item.OnEquipped();
        equippedItem = item;

        return true;
    }

    private bool ValidateItem(InventoryItem item)
    {
        if (item == null) return false;

        return item.Validate();
    }

    private bool DestroyItem(InventoryItem item)
    {
        if (item == null) return true;

        if (inventory.Contains(item)) inventory.Remove(item);

        Destroy(item.gameObject); // for saving persistent data, may change code to instead update a variable indicating the item should no longer exist

        return true;
    }
}

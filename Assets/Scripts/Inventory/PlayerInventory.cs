using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : PersistentObject
{
    public static PlayerInventory Instance { get; private set; }

    private Dictionary<string, ItemData> itemDataReferences = new Dictionary<string, ItemData>();
    private Dictionary<ItemData, string> itemNameReferences = new Dictionary<ItemData, string>();

    [field: SerializeField] public List<InventoryItem> Inventory { get; private set; } = new List<InventoryItem>();

    [SerializeField] private int maxInventorySpace;

    private InventoryItem equippedItem;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            foreach (ItemData itemData in Resources.LoadAll("ItemDataScriptableObjects", typeof(ItemData)))
            {
                itemDataReferences.Add(itemData.ItemName, itemData);
                itemNameReferences.Add(itemData, itemData.ItemName);
            }

            DontDestroyOnLoad(this);
        }
    }

    public event Action<InventoryItem> itemAddedEvent;
    public bool AddItem(InventoryItem newItem)
    {
        if (newItem == null) return false;

        if (Inventory.Contains(newItem)) return false;

        foreach(InventoryItem item in Inventory)
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

        if (ValidateSpace(newItem.CurrentStack))
        {
            Inventory.Add(newItem);
            newItem.OnAdded();
            if (itemAddedEvent != null) itemAddedEvent(newItem);
        }

        return true;
    }

    public event Action<InventoryItem> itemRemovedEvent;
    public bool RemoveItem(InventoryItem removedItem)
    {
        if (removedItem == null) return false;

        if (!Inventory.Contains(removedItem)) return false;

        Inventory.Remove(removedItem);
        removedItem.OnRemoved();
        if (itemRemovedEvent != null) itemRemovedEvent(removedItem);

        return true;
    }
    public bool Clear()
    {
        foreach (InventoryItem item in Inventory)
            RemoveItem(item);

        return true;
    }

    public List<InventoryItem> FindItems(ItemData data)
    {
        return Inventory.Where(item => item.Data == data).ToList();
    }

    public List<InventoryItem> FindItemsByType(ItemType type)
    {
        return Inventory.Where(item => item.Data.Type == type).ToList();
    }

    public event Action<InventoryItem, InventoryItem> itemCombinedEvent;
    public InventoryItem CombineItems(InventoryItem item, InventoryItem otherItem, int initialStack)
    {
        if (item == null || otherItem == null) return null;

        if (!Inventory.Contains(item)) return null;
        
        if (!Inventory.Contains(otherItem)) return null;

        ItemData combinedResult = item.CombineWith(otherItem);

        if (!ValidateItem(item)) DestroyItem(item);
        if (!ValidateItem(otherItem)) DestroyItem(otherItem);

        if (itemCombinedEvent != null) itemCombinedEvent(item, otherItem);

        return CreateInventoryItem(combinedResult, initialStack, string.Empty);
    }

    public bool UseEquipped(GameObject[] others = null)
    {
        return UseItem(equippedItem, others);
    }

    public bool UseItem(InventoryItem item, GameObject[] others = null)
    {
        if (item == null) return true;

        if (!Inventory.Contains(item)) return false;

        bool result = item.Use(others);

        if (!ValidateItem(item)) DestroyItem(item);

        return result;
    }

    public event Action<InventoryItem> itemEquippedEvent;
    public event Action<InventoryItem> itemUnequippedEvent;
    public bool EquipItem(InventoryItem item)
    {
        if (item == null)
        {
            if (equippedItem != null) equippedItem.OnUnequipped();
            if (itemUnequippedEvent != null) itemUnequippedEvent(equippedItem);
            equippedItem = null;

            return true;
        }

        if (!Inventory.Contains(item)) return false;

        if (!item.Data.Equippable) return false;

        if (equippedItem != null) equippedItem.OnUnequipped();
        if (itemUnequippedEvent != null) itemUnequippedEvent(equippedItem);

        equippedItem = item;

        equippedItem.OnEquipped();
        if (itemEquippedEvent != null) itemEquippedEvent(item);

        return true;
    }

    private bool ValidateItem(InventoryItem item)
    {
        if (item == null) return false;

        return item.Validate();
    }

    private bool ValidateSpace(int requiredSpace)
    {
        int availableSpace = maxInventorySpace;

        foreach (InventoryItem item in Inventory)
        {
            availableSpace -= item.Data.RequiredSpace;
        }

        return availableSpace >= requiredSpace;
    }

    public event Action<InventoryItem> itemDestroyedEvent;
    private bool DestroyItem(InventoryItem item)
    {
        if (item == null) return true;

        if (Inventory.Contains(item)) Inventory.Remove(item);

        item.OnDestroyed();
        if (itemDestroyedEvent != null) itemDestroyedEvent(item);

        return true;
    }

    public override PersistentObjectData Save()
    {
        List<string> data = new List<string>();

        data.Add(maxInventorySpace.ToString());
        data.Add(
            equippedItem != null ?
            ItemDataToString(equippedItem.Data) + "|" + equippedItem.CurrentStack.ToString() + "|" + equippedItem.InstanceID :
            string.Empty);

        foreach (InventoryItem item in Inventory)
        {
            data.Add(ItemDataToString(item.Data) + "|" + item.CurrentStack.ToString() + "|" + item.InstanceID);
        }

        return new PersistentObjectData(data.ToArray());
    }

    public override void Load(PersistentObjectData POData)
    {
        maxInventorySpace = int.Parse(POData.data[0]);

        string[] equippedParsedData = POData.data[1] != string.Empty ? POData.data[1].Split("|") : null;
        string[] parsedData;

        for (int i = 2; i > POData.data.Length; i++)
        {
            parsedData = POData.data[i].Split("|");

            InventoryItem item = CreateInventoryItem(StringToItemData(parsedData[0]), int.Parse(parsedData[1]), parsedData[2]);
            AddItem(item);

            if (equippedParsedData == null) continue;

            if (equippedParsedData[0] == parsedData[0] && equippedParsedData[1] == parsedData[1]) equippedItem = item;
        }
    }

    private string ItemDataToString(ItemData data)
    {
        return itemNameReferences.ContainsKey(data) ? itemNameReferences[data] : string.Empty;
    }

    private ItemData StringToItemData(string name)
    {
        return itemDataReferences.ContainsKey(name) ? itemDataReferences[name] : null;
    }

    public static InventoryItem CreateInventoryItem(ItemData itemData, int initialStack, string instanceID)
    {
        InventoryItem item = null;

        switch (itemData.Type)
        {
            case ItemType.Weapon:
                item = new WeaponItem(itemData, initialStack, instanceID);
                break;
            case ItemType.PuzzlePiece:
                item = new PuzzlePieceItem(itemData, initialStack, instanceID);
                break;
            case ItemType.Consumable:
                item = new ConsumableItem(itemData, initialStack, instanceID);
                break;
            case ItemType.Notes:
                item = new NotesItem(itemData, initialStack, instanceID);
                break;
        }

        return item;
    }
}

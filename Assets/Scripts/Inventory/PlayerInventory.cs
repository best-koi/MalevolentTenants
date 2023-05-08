using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : PersistentObject
{
    public static PlayerInventory Instance { get; private set; }

    [SerializeField] private List<ItemData> references;

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

            references = ScriptableObjectUtilities.FindAllScriptableObjectsOfType<ItemData>("t:ItemData", "Assets/Scripts/Inventory/ItemData/ScriptableObjects");

            DontDestroyOnLoad(this);
        }
    }

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
            newItem.OnAdded();
            Inventory.Add(newItem);
        }

        return true;
    }

    public bool RemoveItem(InventoryItem removedItem)
    {
        if (removedItem == null) return false;

        if (!Inventory.Contains(removedItem)) return false;

        removedItem.OnRemoved();
        Inventory.Remove(removedItem);

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
        List<InventoryItem> foundItems = new List<InventoryItem>();

        foreach (InventoryItem item in Inventory)
        {
            if (item.Data == data)
            {
                foundItems.Add(item);
            }
        }

        return foundItems;
    }

    public List<InventoryItem> FindItemsByType(ItemType type)
    {
        List<InventoryItem> foundItems = new List<InventoryItem>();

        foreach (InventoryItem item in Inventory)
        {
            if (item.Data.Type == type)
            {
                foundItems.Add(item);
            }
        }

        return foundItems;
    }

    public InventoryItem CombineItems(InventoryItem item, InventoryItem otherItem, int initialStack)
    {
        if (item == null || otherItem == null) return null;

        if (!Inventory.Contains(item)) return null;
        
        if (!Inventory.Contains(otherItem)) return null;

        ItemData combinedResult = item.CombineWith(otherItem);

        if (!ValidateItem(item)) DestroyItem(item);
        if (!ValidateItem(otherItem)) DestroyItem(otherItem);

        return CreateInventoryItem(combinedResult, initialStack);
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

    public bool EquipItem(InventoryItem item)
    {
        if (item == null)
        {
            equippedItem = null;
            return true;
        }

        if (!Inventory.Contains(item)) return false;

        if (!item.Data.Equippable) return false;

        if (equippedItem != null) equippedItem.OnUnequipped();
        item.OnEquipped();
        equippedItem = item;

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

    private bool DestroyItem(InventoryItem item)
    {
        if (item == null) return true;

        if (Inventory.Contains(item)) Inventory.Remove(item);

        item.OnDestroyed();

        return true;
    }

    public override PersistentObjectData Save()
    {
        List<string> data = new List<string>();

        data.Add(maxInventorySpace.ToString());
        data.Add(
            equippedItem != null ?
            equippedItem.Data.ItemID + "|" + equippedItem.CurrentStack.ToString() :
            string.Empty);

        foreach (InventoryItem item in Inventory)
        {
            data.Add(item.Data.ItemID + "|" + item.CurrentStack.ToString());
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

            InventoryItem item = CreateInventoryItem(LoadItemFromID(parsedData[0]), int.Parse(parsedData[1]));
            AddItem(item);

            if (equippedParsedData == null) continue;

            if (equippedParsedData[0] == parsedData[0] && equippedParsedData[1] == parsedData[1]) equippedItem = item;
        }
    }

    private ItemData LoadItemFromID(string ID)
    {
        foreach (ItemData item in references)
            if (item.ItemID == ID) return item;

        return null;
    }

    public static InventoryItem CreateInventoryItem(ItemData itemData, int initialStack)
    {
        InventoryItem item = null;

        switch (itemData.Type)
        {
            case ItemType.Weapon:
                item = new WeaponItem(itemData, initialStack);
                break;
            case ItemType.PuzzlePiece:
                item = new PuzzlePieceItem(itemData, initialStack);
                break;
            case ItemType.Consumable:
                item = new ConsumableItem(itemData, initialStack);
                break;
            case ItemType.Notes:
                item = new NotesItem(itemData, initialStack);
                break;
        }

        return item;
    }
}

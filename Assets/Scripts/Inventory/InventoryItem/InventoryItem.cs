using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class InventoryItem
{
    [field: SerializeField] public ItemData Data { get; private set; }

    [field: SerializeField] public string InstanceID { get; private set; }

    public int CurrentStack
    {
        get { return currentStack; }
        set { currentStack = Mathf.Clamp(value, 0, Data.MaxStack); }
    }
    [SerializeField] private int currentStack = 1;

    static List<InventoryItem> allInventoryItemInstances = new List<InventoryItem>();

    public InventoryItem(ItemData data, int initialStack, string instanceID)
    {
        Data = data;

        CurrentStack = initialStack;

        InstanceID = instanceID != string.Empty ? instanceID : Guid.NewGuid().ToString();

        allInventoryItemInstances.Add(this);
    }

    public abstract bool Use(GameObject[] objs = null);

    public virtual void OnAdded()
    {

    }

    public virtual void OnRemoved()
    {

    }

    public virtual void OnCombined()
    {
        CurrentStack -= 1;
    }

    public virtual void OnEquipped()
    {

    }

    public virtual void OnUnequipped()
    {

    }

    public virtual void OnDestroyed()
    {

    }

    public ItemData CombineWith(InventoryItem otherItem)
    {
        if (!Data.ValidateCombinations(otherItem.Data)) return null;

        OnCombined();
        otherItem.OnCombined();

        return Data.CombinedResults[Data.CombinableItems.IndexOf(otherItem.Data)];
    }

    public virtual bool Validate()
    {
        if (CurrentStack <= 0) return false;

        return true;
    }

    public static InventoryItem FindItemByID(string instanceID)
    {
        foreach (InventoryItem item in allInventoryItemInstances)
            if (item.InstanceID == instanceID) return item;

        return null;
    }
}

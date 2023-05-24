using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConsumableItem : InventoryItem
{
    [field: SerializeField] public ConsumableData data { get; protected set; }

    public ConsumableItem(ItemData data, int initialStack, string instanceID) : base(data, initialStack, instanceID)
    {
        data = (ConsumableData)Data;
    }

    public override bool Use(GameObject[] others = null)
    {
        // TO DO: Do different actions based on (a yet created) enum var in ConsumableData

        CurrentStack = CurrentStack - 1;

        return true;
    }
}

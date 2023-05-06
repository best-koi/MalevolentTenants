using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : InventoryItem
{
    [SerializeField] protected ConsumableData data;

    public ConsumableItem(ItemData data, int initialStack) : base(data, initialStack)
    {
        data = (ConsumableData)Data;
    }

    public override bool Use(GameObject[] others = null)
    {
        // TO DO: Do different actions based on (a yet created) enum var in ConsumableData

        return true;
    }
}

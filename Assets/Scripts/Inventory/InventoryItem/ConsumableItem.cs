using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableItem : InventoryItem
{
    [SerializeField] protected ConsumableData data;

    protected void Awake()
    {
        data = (ConsumableData)Data;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NotesItem : InventoryItem
{
    [field: SerializeField] public NotesData data { get; protected set; }

    public NotesItem(ItemData data, int initialStack, string instanceID) : base(data, initialStack, instanceID)
    {
        data = (NotesData)Data;
    }

    public override bool Use(GameObject[] others = null)
    {
        // possibly show description on a bigger screen than for other items

        return true;
    }
}

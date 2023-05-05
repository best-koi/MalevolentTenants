using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesItem : InventoryItem
{
    [SerializeField] protected NotesData data;

    protected void Awake()
    {
        data = (NotesData)Data;
    }

    public override bool Use(GameObject[] others = null)
    {
        // possibly show description on a bigger screen than for other items

        return true;
    }
}

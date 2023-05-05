using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : InventoryItem
{
    public override bool Use(GameObject[] others = null)
    {
        Debug.LogFormat("TestItem: Use method was called");

        return true;
    }
}

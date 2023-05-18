using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponItem : InventoryItem
{
    [SerializeField] protected WeaponData data;

    public WeaponItem(ItemData data, int initialStack, string instanceID) : base(data, initialStack, instanceID)
    {
        data = (WeaponData)Data;
    }

    public override bool Use(GameObject[] others = null)
    {
        // melee hit

        return true;
    }
}
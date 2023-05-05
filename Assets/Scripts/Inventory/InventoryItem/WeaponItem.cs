using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : InventoryItem
{
    [SerializeField] protected WeaponData data;

    protected void Awake()
    {
        data = (WeaponData)Data;
    }

    public override bool Use(GameObject[] others = null)
    {
        // melee hit

        return true;
    }
}

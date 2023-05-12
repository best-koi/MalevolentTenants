using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConsumableData", menuName = "ItemData/ConsumableData")]
public class ConsumableData : ItemData
{
    protected override void Awake()
    {
        base.Awake();

        Type = ItemType.Consumable;
    }
}

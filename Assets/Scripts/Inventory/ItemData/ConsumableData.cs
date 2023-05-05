using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ConsumableData", menuName = "ItemData/ConsumableData")]
public class ConsumableData : ItemData
{
    private void Awake()
    {
        Type = ItemType.Consumable;
    }
}

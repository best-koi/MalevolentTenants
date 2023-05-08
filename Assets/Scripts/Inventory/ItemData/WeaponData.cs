using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "ItemData/WeaponData")]
public class WeaponData : ItemData
{
    [field: SerializeField] public float damage { get; protected set; }

    protected override void Awake()
    {
        base.Awake();

        Type = ItemType.Weapon;
        Equippable = true;
    }
}

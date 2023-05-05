using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "ItemData/WeaponData")]
public class WeaponData : ItemData
{
    [field: SerializeField] public float damage { get; protected set; }

    private void Awake()
    {
        Type = ItemType.Weapon;
    }
}

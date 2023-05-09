using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    [field: SerializeField] public string ItemName { get; protected set; }

    public string Description 
    {
        get { return description; }
        protected set { description = value; } 
    }
    [TextArea(10, 10)]
    [SerializeField] private string description;

    public ItemType Type { get; protected set; }

    [field: SerializeField] public Sprite Icon { get; protected set; }

    [field: SerializeField] public List<ItemData> CombinableItems { get; protected set; }
    [field: SerializeField] public List<ItemData> CombinedResults { get; protected set; }

    public int MaxStack
    {
        get { return Mathf.Max(1, maxStack); }
        protected set { maxStack = value; }
    }
    [SerializeField] private int maxStack = 1;

    public int RequiredSpace 
    { 
        get { return Mathf.Max(0, requiredSpace); }
        protected set { requiredSpace = value; } 
    }
    [SerializeField] private int requiredSpace = 1;

    [field: SerializeField] public bool Equippable { get; protected set; }

    public bool ValidateCombinations(ItemData other)
    {
        if (!CombinableItems.Contains(other)) return false;

        if (CombinableItems.Count != CombinedResults.Count) return false;

        List<ItemData> distinctOthers = new List<ItemData>();

        foreach (ItemData data in CombinableItems)
            if (!distinctOthers.Contains(data)) distinctOthers.Add(data);

        if (CombinableItems.Count != distinctOthers.Count) return false;

        return true;
    }

    protected virtual void Awake()
    {

    }
}

public enum ItemType
{
    Weapon,
    PuzzlePiece,
    Consumable,
    Notes
}
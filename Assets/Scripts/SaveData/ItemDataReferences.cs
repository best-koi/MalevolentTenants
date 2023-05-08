using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemDataReferences", menuName = "ItemData/ItemDataReferences")]
public class ItemDataReferences : ScriptableObject
{
    [SerializeField] private List<ItemData> references;

    public int GetIndex(ItemData data)
    {
        for (int i = 0; i < references.Count; i++)
            if (references[i] == data) return i;

        return -1;
    }

    public ItemData GetItemData(int i)
    {
        ItemData data = null;

        if (i >= 0 && i < references.Count)
            data = references[i];

        return references[i];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemCombining : MonoBehaviour
{
    [SerializeField] private InventoryItem item;

    [SerializeField] private InventoryItem otherItem;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject combinedResult = PlayerInventory.Instance.CombineItems(item, otherItem);

            if (combinedResult != null)
                Instantiate(combinedResult, Vector3.zero, Quaternion.identity);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GameObject combinedResult = PlayerInventory.Instance.CombineItems(otherItem, item);

            if (combinedResult != null)
                Instantiate(combinedResult, Vector3.zero, Quaternion.identity);
        }
    }
}

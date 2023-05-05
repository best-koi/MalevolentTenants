using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemPickup : MonoBehaviour
{
    [SerializeField] InventoryItem item;

    [SerializeField] InventoryItem otherItem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && item != null)
        {
            item.Interact();
        }
        else if (Input.GetKeyDown(KeyCode.E) && otherItem != null)
        {
            otherItem.Interact();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && item != null)
        {
            PlayerInventory.Instance.RemoveItem(item);
        }
        else if (Input.GetKeyDown(KeyCode.C) && otherItem != null)
        {
            PlayerInventory.Instance.RemoveItem(otherItem);
        }
    }
}

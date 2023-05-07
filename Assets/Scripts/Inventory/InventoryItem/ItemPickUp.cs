using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;

    [SerializeField] private int initialStack = 1;

    private InventoryItem createdItem;

    private void Awake()
    {

    }

    public bool Interact()
    {
        createdItem = PlayerInventory.CreateInventoryItem(itemData, initialStack);
        createdItem.itemAdded += Added;

        PlayerInventory.Instance.AddItem(createdItem);

        return true;
    }

    protected virtual void Added()
    {
        SceneObject so = GetComponent<SceneObject>();

        if (so != null)
            so.isActive = false;

        gameObject.SetActive(so.isActive);
    }
}

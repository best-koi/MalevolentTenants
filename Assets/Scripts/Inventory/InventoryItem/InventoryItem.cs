using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : MonoBehaviour, IInteractable
{
    [field: SerializeField] public ItemData Data { get; private set; }

    public int CurrentStack
    {
        get { return currentStack; }
        set { currentStack = Mathf.Clamp(value, 0, Data.MaxStack); }
    }
    [SerializeField] private int currentStack = 1;

    public abstract bool Use(GameObject[] objs = null);

    public virtual void OnAdded()
    {
        // will change for data persistence

        gameObject.SetActive(false);
    }

    public virtual void OnRemoved()
    {
        // will change for data persistence

        gameObject.SetActive(true);
    }

    public virtual void OnCombined()
    {
        CurrentStack -= 1;
    }

    public virtual void OnEquipped()
    {

    }

    public GameObject CombineWith(InventoryItem otherItem)
    {
        if (!Data.ValidateCombinations(otherItem.Data)) return null;

        OnCombined();
        otherItem.OnCombined();

        return Data.CombinedResults[Data.CombinableItems.IndexOf(otherItem.Data)];
    }

    public virtual bool Validate()
    {
        if (CurrentStack <= 0) return false;

        return true;
    }

    public bool Interact()
    {
        PlayerInventory.Instance.AddItem(this);

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemPickup : MonoBehaviour
{
    [SerializeField] ItemPickUp item;

    [SerializeField] ItemPickUp otherItem;

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
    }
}

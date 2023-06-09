using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleTrigger : MonoBehaviour
{
    public bool solved = false;
    void OnTriggerEnter (Collider other)
    {
        solved = true;
        InGameUIManager.Instance.DoorUnlocked();
        //Debug.Log("Door Unlocked!");
    }
}

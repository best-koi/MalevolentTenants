using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Door_work : MonoBehaviour
{
    public PuzzleTrigger ReferencePuzzle;
    void OnTriggerEnter(Collider other)
    {
        if (ReferencePuzzle.solved)
            InGameUIManager.Instance.YouEscapedToggle(true);
         //Debug.Log("You Win");
    }
}

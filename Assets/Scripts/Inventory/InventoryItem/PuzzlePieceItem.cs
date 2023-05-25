using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzlePieceItem : InventoryItem
{
    [field: SerializeField] public PuzzlePieceData data { get; protected set; }

    public PuzzlePieceItem(ItemData data, int initialStack, string instanceID) : base(data, initialStack, instanceID)
    {
        data = (PuzzlePieceData)Data;
    }

    public override bool Use(GameObject[] others = null)
    {
        /*Pseudocode
        
        if (others.Length == 0) return false;

        Puzzle puzzle = others[0].GetComponent<Puzzle>();
        
        if (puzzle == null) return false;
        
        bool result = puzzle.Solve(data);

        if (result)
        {
            CurrentStack -= 1;
        }
       
        return result;
        */
        
        CurrentStack = CurrentStack - 1;

        return true; // Delete if implementing pseudocode
    }
}
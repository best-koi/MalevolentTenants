using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceItem : InventoryItem
{
    [SerializeField] protected PuzzlePieceData data;

    protected void Awake()
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

        return true; // Delete if implementing pseudocode
    }
}
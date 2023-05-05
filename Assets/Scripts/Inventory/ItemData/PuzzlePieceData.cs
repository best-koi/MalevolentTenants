using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PuzzlePieceData", menuName = "ItemData/PuzzlePieceData")]
public class PuzzlePieceData : ItemData
{
    private void Awake()
    {
        Type = ItemType.PuzzlePiece;
    }
}
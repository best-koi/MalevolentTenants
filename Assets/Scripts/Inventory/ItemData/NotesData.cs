using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NotesData", menuName = "ItemData/NotesData")]
public class NotesData : ItemData
{
    private void Awake()
    {
        Type = ItemType.Notes;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NotesData", menuName = "ItemData/NotesData")]
public class NotesData : ItemData
{
    protected override void Awake()
    {
        base.Awake();

        Type = ItemType.Notes;

        RequiredSpace = 0;
    }
}

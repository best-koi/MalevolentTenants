using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistentObject : MonoBehaviour
{
    public abstract PersistentObjectData Save();

    public abstract void Load(PersistentObjectData POData);
}

[System.Serializable]
public class PersistentObjectData
{
    public string[] data;

    public PersistentObjectData(string[] data)
    {
        this.data = data;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneObject : MonoBehaviour
{
    public abstract SceneObjectData Save();

    public abstract void Load(SceneObjectData SOData);
}

[System.Serializable]
public class SceneObjectData
{
    public string[] data;

    public SceneObjectData(string[] data)
    {
        this.data = data;
    }
}
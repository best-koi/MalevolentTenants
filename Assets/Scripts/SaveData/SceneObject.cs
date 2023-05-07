using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    public bool isActive = true;

    public SceneObjectData Save()
    {
        return new SceneObjectData(isActive);
    }

    public void Load(SceneObjectData data)
    {
        isActive = data.isActive;

        gameObject.SetActive(isActive);
    }
}

[System.Serializable]
public class SceneObjectData
{
    public bool isActive;

    public SceneObjectData(bool isActive)
    {
        this.isActive = isActive;
    }
}
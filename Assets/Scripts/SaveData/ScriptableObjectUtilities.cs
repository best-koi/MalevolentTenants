using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class ScriptableObjectUtilities
{
    public static List<T> FindAllScriptableObjectsOfType<T>(string filter, string folder = "Assets")
        where T : ScriptableObject
    {
        return AssetDatabase.FindAssets(filter, new[] { folder })
            .Select(guid => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList();
    }
}
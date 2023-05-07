using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneSaveManager : MonoBehaviour
{
    [SerializeField] private List<SceneObject> persistentObjects = new List<SceneObject>();

    private string directory;
    
    private string fileName;

    private void Awake()
    {
        EditorApplication.playModeStateChanged += DeleteSave;

        SceneManager.sceneUnloaded += Save;

        directory = "/SaveData/";
        fileName = string.Format("Scene{0}Data.txt", SceneManager.GetActiveScene().name);
        
        Load();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(0);
        else if (Input.GetMouseButtonDown(1))
            SceneManager.LoadScene(1);
    }

    private void Save(Scene current)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        List<SceneObjectData> persistentObjData = new List<SceneObjectData>();

        foreach (SceneObject obj in persistentObjects)
            persistentObjData.Add(obj.Save());

        string json = JsonUtility.ToJson(new SceneData(SceneManager.GetActiveScene().buildIndex, persistentObjData), true);

        File.WriteAllText(dir + fileName, json);
    }

    private void Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);

            SceneData data = JsonUtility.FromJson<SceneData>(json);

            for (int i = 0; i < data.persistentObjectData.Count; i++)
                persistentObjects[i].Load(data.persistentObjectData[i]);
        }
        else
            Debug.LogFormat("SceneSaveManager.Load: Save file does not exist");
    }

    private void DeleteSave(PlayModeStateChange state)
    {
        if (state != PlayModeStateChange.ExitingPlayMode) return;

        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + directory);

        foreach (FileInfo file in dir.GetFiles())
        {
            file.Delete();
        }
    }
}

[System.Serializable]
public class SceneData
{
    public int sceneIndex;

    public List<SceneObjectData> persistentObjectData;

    public SceneData(int index, List<SceneObjectData> data)
    {
        sceneIndex = index;

        persistentObjectData = data;
    }
}

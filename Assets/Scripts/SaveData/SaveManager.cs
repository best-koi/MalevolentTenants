using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private List<PersistentObject> persistentSceneObjects = new List<PersistentObject>();

    private string directory = "/SaveData/";

    private string inventoryFileName = "InventorySaveData.txt";

    private string sceneFileName;

    private void Awake()
    {
        SceneManager.sceneUnloaded += Save;

        sceneFileName = string.Format("{0}SceneSaveData.txt", SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        Load();
    }

    private void Save(Scene current)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string json = JsonUtility.ToJson(PlayerInventory.Instance.Save(), true);
        File.WriteAllText(dir + inventoryFileName, json);


        List<PersistentObjectData> persistentObjData = new List<PersistentObjectData>();

        foreach (PersistentObject obj in persistentSceneObjects)
            persistentObjData.Add(obj.Save());

        json = JsonUtility.ToJson(new SceneData(persistentObjData), true);
        File.WriteAllText(dir + sceneFileName, json);

        SceneManager.sceneUnloaded -= Save;
    }

    private void Load()
    {
        string fullPath = Application.persistentDataPath + directory + inventoryFileName;

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);

            PersistentObjectData data = JsonUtility.FromJson<PersistentObjectData>(json);

            PlayerInventory.Instance.Load(data);
        }


        fullPath = Application.persistentDataPath + directory + sceneFileName;

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);

            SceneData data = JsonUtility.FromJson<SceneData>(json);

            for (int i = 0; i < data.persistentObjectData.Count; i++)
                persistentSceneObjects[i].Load(data.persistentObjectData[i]);
        }
        else
            Debug.LogFormat("SaveManager.Load: Save file does not exist");
    }

    private void DeleteSave()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + directory);

        if (dir == null) return;

        foreach (FileInfo file in dir.GetFiles())
        {
            file.Delete();
        }
    }

    private void OnApplicationQuit()
    {
        DeleteSave();
    }
}

[System.Serializable]
public class SceneData
{
    public List<PersistentObjectData> persistentObjectData;

    public SceneData(List<PersistentObjectData> data)
    {
        persistentObjectData = data;
    }
}

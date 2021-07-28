using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    //The following design pattern is called "singleton" ensuring only a single Instance of a game object exists
    //In this case it is a static instance for persistant data storage between scenes

    public static DataManager Instance;

    public string playerName;
    public int highScore;

    void Awake()
    {
        //Delete new instances
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        //Set reference to the existing instance
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }
    //[System.Serializable]
    //class SaveData
    //{
    //    public Color TeamColor;
    //}

    //public void SaveColor()
    //{
    //    SaveData data = new SaveData();
    //    data.TeamColor = TeamColor;

    //    string json = JsonUtility.ToJson(data);

    //    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    //}

    //public void LoadColor()
    //{
    //    string path = Application.persistentDataPath + "/savefile.json";
    //    if (File.Exists(path))
    //    {
    //        string json = File.ReadAllText(path);
    //        SaveData data = JsonUtility.FromJson<SaveData>(json);

    //        TeamColor = data.TeamColor;
    //    }
    //}
}
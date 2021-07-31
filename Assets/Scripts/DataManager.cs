using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class DataManager : MonoBehaviour
{
    //The following design pattern is called "singleton" ensuring only a single Instance of a game object exists
    //In this case it is a static instance for persistant data storage between scenes

    public static DataManager Instance;

    public string playerName;
    public string highScoreOwner;
    public int highScore;

    public TextMeshProUGUI scoreDisplay;
    public TMP_Text  playerNameField;

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
        LoadScoreData();
    }

    public void FillHighScore()
    {
        scoreDisplay.text = GetHighScoreInfo();
    }

    public string GetHighScoreInfo()
    {
        string highScoreInfo = "Best Score: " + highScoreOwner + " : " + highScore ;
        return highScoreInfo;
    }

    public void GetPlayerName()
    {
        playerName = playerNameField.text;
    }

    [System.Serializable]
    class SaveData
    {
        //public Color TeamColor;
        public string playerName;
        public string highScoreOwner;
        public int highScore;
    }

    public void SaveScoreData()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.highScoreOwner = highScoreOwner;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //TeamColor = data.TeamColor;
            playerName = data.playerName;
            highScoreOwner = data.highScoreOwner;
            highScore = data.highScore;
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
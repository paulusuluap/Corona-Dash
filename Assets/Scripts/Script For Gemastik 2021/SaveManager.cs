using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }

    [Header("Save & Load Settings")]
    [SerializeField] private bool[] worldsUnlocked = new bool[5] {true, false, false, false, false};
    public bool[] WorldsUnlocked {get {return worldsUnlocked;} set {worldsUnlocked = value;}}
    [HideInInspector] public int playerMoney, gainedMoney;
    [HideInInspector] public string language;

    private void Awake() {
        Instance = this;
        Load();
    }

    public void Save()
    {
        PlayerData_Storage saveData = new PlayerData_Storage();

        saveData.worldsUnlocked = worldsUnlocked;
        saveData.playerMoney = playerMoney;
        saveData.gainedMoney = gainedMoney;
        saveData.language = language;

        //Converting to JSON
        string jsonData = JsonUtility.ToJson(saveData);

        //Save JSON String
        PlayerPrefs.SetString("MySettings", jsonData);
        PlayerPrefs.Save();
        // Debug.Log("Saved");
    }

    public void Load()
    {
        string jsonData = PlayerPrefs.GetString("MySettings");
        PlayerData_Storage loadedData = JsonUtility.FromJson<PlayerData_Storage>(jsonData);
        
        //Data controller
        if(loadedData == null)
        {
            worldsUnlocked = new bool[5] {true, false, false, false, false};;
            playerMoney = 0;
            gainedMoney = 0;
            language = "English";
        }
        else
        {
            worldsUnlocked = loadedData.worldsUnlocked;
            playerMoney = loadedData.playerMoney;
            gainedMoney = loadedData.gainedMoney;
            language = loadedData.language;
            //highscore
        }
    }
}

[Serializable]
public class PlayerData_Storage
{
    public int playerMoney;
    public int gainedMoney;
    public bool[] worldsUnlocked;
    public string language;
}

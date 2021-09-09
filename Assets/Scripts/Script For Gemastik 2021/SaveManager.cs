using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }

    [Header("Save & Load Settings")]
    
    [SerializeField] 
    private bool[] worldsUnlocked = new bool[5] {true, false, false, false, false};
    public bool[] WorldsUnlocked 
    {
        get =>  worldsUnlocked;
        set => worldsUnlocked = value;
    }
    [HideInInspector] public int playerMoney, gainedMoney;
    [HideInInspector] public string language;
    
    // Power Ups Durations
    private float magnetDuration, maskDuration, invincibleDuration, multiplierDuration;
    public float MagnetDuration{
        get => magnetDuration;
        set => magnetDuration = value;
    }
    public float MaskDuration{
        get => maskDuration;
        set => maskDuration = value;
    }
    public float InvincibleDuration{
        get => invincibleDuration;
        set => invincibleDuration = value;
    }
    public float MultiplierDuration{
        get => multiplierDuration;
        set => multiplierDuration = value;
    }
    
    //Power Ups Level
    private int m_MagnetLevel;
    public int MagnetLevel {
        get => m_MagnetLevel;
        set => m_MagnetLevel = value;
    }
    private int m_MaskLevel;
    public int MaskLevel {
        get => m_MaskLevel;
        set => m_MaskLevel = value;
    }
    private int m_InvincibleLevel;
    public int InvincibleLevel {
        get => m_InvincibleLevel;
        set => m_InvincibleLevel = value;
    }
    private int m_MultiplierLevel;
    public int MultiplierLevel {
        get => m_MultiplierLevel;
        set => m_MultiplierLevel = value;
    }

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

        saveData.magnetDuration = magnetDuration;
        saveData.maskDuration = maskDuration;
        saveData.invincibleDuration = invincibleDuration;
        saveData.multiplierDuration = multiplierDuration;

        saveData.magnetLevel = m_MagnetLevel;
        saveData.maskLevel = m_MaskLevel;
        saveData.invincibleLevel = m_InvincibleLevel;
        saveData.multiplierLevel = m_MultiplierLevel;

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
            worldsUnlocked = new bool[5] {true, false, false, false, false};
            playerMoney = 0;
            gainedMoney = 0;
            language = "English";

            magnetDuration = 10f;
            maskDuration = 10f;
            invincibleDuration = 10f;
            multiplierDuration = 10f;

            m_MagnetLevel = 0;
            m_MaskLevel = 0;
            m_InvincibleLevel = 0;
            m_MultiplierLevel = 0;
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
    
    //Power Ups Durations
    public float magnetDuration;
    public float maskDuration;
    public float invincibleDuration;
    public float multiplierDuration;

    //Power Ups Level
    public int magnetLevel;
    public int maskLevel;
    public int invincibleLevel;
    public int multiplierLevel;
}

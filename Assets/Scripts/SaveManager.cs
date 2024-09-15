using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Dynamic;


public class SaveManager : MonoBehaviour
{
    SoundController soundController;
    SettingsController settingsController;

    public bool autoLoadSave;
    public static SaveManager sm;
    public SaveData saveData;

    public string saveName;


    PlayerManager playerOW;
    UnderworldControler playerUW;

    public bool isMenu;

    WorldManager wm;
    void Awake()
    {
        sm = this;
    }

    void Start()
    {
        playerOW = PlayerManager.player;
        playerUW = UnderworldControler.player;
        wm = WorldManager.wm;
        soundController = SoundController.soundController;
        settingsController = SettingsController.settingsController;

        Debug.Log("start");
        ReadData(saveName);
        if (autoLoadSave)
        {
            SetData();
        }


    }

    public void ResetData()
    {
        float tempSound = saveData.soundVolume;
        float tempMusic = saveData.musicVolume;

        saveData = new SaveData();
        saveData.inOverworld = true;
        saveData.Day = 1;

        saveData.soundVolume = tempSound;
        saveData.musicVolume = tempMusic;

        WriteData();
    }

    public void CollectData()
    {
        if (!isMenu)
        {
            if (wm.inOverworld)
            {
                saveData.Money = playerOW.Money;
                saveData.TotalMoney = playerOW.TotalMoney;
                saveData.Day = playerOW.day;
                saveData.Depth = playerOW.fishingRod.Depth;
                saveData.Capacity = playerOW.fishingRod.Capacity;
                saveData.Power = playerOW.fishingRod.Power;
            }
            else
            {
                saveData.Money = playerUW.Money;
                saveData.TotalMoney = playerUW.TotalMoney;
                saveData.Day = playerUW.day;
                saveData.Depth = playerUW.fishingRod.Depth;
                saveData.Capacity = playerUW.fishingRod.Capacity;
                saveData.Power = playerUW.fishingRod.Power;
            }

            saveData.inOverworld = wm.inOverworld;
        }

        saveData.soundVolume = settingsController.SoundVolume();
        soundController.soundSettingsVolume = saveData.soundVolume;

        saveData.musicVolume = settingsController.MusicVolume();
        soundController.musicSettingsVolume = saveData.musicVolume;

    }

    public void SetData()
    {
        if (!isMenu)
        {
            if (wm.inOverworld)
            {
                playerOW.Money = saveData.Money;
                playerOW.TotalMoney = saveData.TotalMoney;
                playerOW.day = saveData.Day;
                playerOW.fishingRod.Depth = saveData.Depth;
                playerOW.fishingRod.Capacity = saveData.Capacity;
                playerOW.fishingRod.Power = saveData.Power;
            }
            else
            {
                playerUW.Money = saveData.Money;
                playerUW.TotalMoney = saveData.TotalMoney;
                playerUW.day = saveData.Day;
                playerUW.fishingRod.Depth = saveData.Depth;
                playerUW.fishingRod.Capacity = saveData.Capacity;
                playerUW.fishingRod.Power = saveData.Power;
            }
        }

        saveData.soundVolume = settingsController.SoundVolume();
        soundController.soundSettingsVolume = saveData.soundVolume;

        saveData.musicVolume = settingsController.MusicVolume();
        soundController.musicSettingsVolume = saveData.musicVolume;
    }

    public void WriteData()
    {
        RewriteFile("save", JsonUtility.ToJson(saveData));
    }

    static void RewriteFile(string name, string value)
    {
        string path = Application.dataPath + "/Resources/" + name + ".txt";
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(value);
        writer.Close();
    }
    public void ReadData(string name)
    {
        //Resources.UnloadUnusedAssets();
        string path = Application.dataPath + "/Resources/" + name + ".txt";
        Debug.Log(path);

        //Resources.Load(path);

        string dataToLoad = "";
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                dataToLoad = reader.ReadToEnd();
            }
        }
        //TextAsset asset = Resources.Load<TextAsset>(name);

        saveData = JsonUtility.FromJson<SaveData>(dataToLoad);
        Debug.Log(saveData.Depth);
        Debug.Log(saveData.Money);
    }

}
[System.Serializable]
public class SaveData
{
    public float Money;
    public float TotalMoney;
    public int Day;
    public int Depth;
    public int Capacity;
    public int Power;
    public bool inOverworld;
    public float soundVolume;
    public float musicVolume;

}
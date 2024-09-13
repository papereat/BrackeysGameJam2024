using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class SaveManager : MonoBehaviour
{
    public bool autoLoadSave;
    public static SaveManager sm;
    public SaveData saveData;

    public string saveName;


    PlayerManager playerOW;
    UnderworldControler playerUW;


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

        ReadData(saveName);
        if (autoLoadSave)
        {
            SetData();
        }


    }

    public void CollectData()
    {
        if (wm.inOverworld)
        {
            saveData.Money = playerOW.Money;
            saveData.Day = playerOW.day;
            saveData.Depth = playerOW.fishingRod.Depth;
            saveData.Capacity = playerOW.fishingRod.Capacity;
            saveData.Power = playerOW.fishingRod.Power;
        }
        else
        {
            saveData.Money = playerUW.Money;
            saveData.Day = playerUW.day;
            saveData.Depth = playerUW.fishingRod.Depth;
            saveData.Capacity = playerUW.fishingRod.Capacity;
            saveData.Power = playerUW.fishingRod.Power;
        }

        saveData.inOverworld = wm.inOverworld;


    }

    public void SetData()
    {
        if (wm.inOverworld)
        {
            playerOW.Money = saveData.Money;
            playerOW.day = saveData.Day;
            playerOW.fishingRod.Depth = saveData.Depth;
            playerOW.fishingRod.Capacity = saveData.Capacity;
            playerOW.fishingRod.Power = saveData.Power;
        }
        else
        {
            playerUW.Money = saveData.Money;
            playerUW.day = saveData.Day;
            playerUW.fishingRod.Depth = saveData.Depth;
            playerUW.fishingRod.Capacity = saveData.Capacity;
            playerUW.fishingRod.Power = saveData.Power;
        }
    }

    public void WriteData()
    {
        RewriteFile("save", JsonUtility.ToJson(saveData));
    }

    static void RewriteFile(string name, string value)
    {
        string path = "Assets/Resources/" + name + ".txt";
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(value);
        writer.Close();
    }
    public void ReadData(string name)
    {
        string path = "Assets/Resources/" + name;

        AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load<TextAsset>(name);

        saveData = JsonUtility.FromJson<SaveData>(asset.text);
    }

}
[System.Serializable]
public class SaveData
{
    public float Money;
    public int Day;
    public int Depth;
    public int Capacity;
    public int Power;
    public bool inOverworld;
}
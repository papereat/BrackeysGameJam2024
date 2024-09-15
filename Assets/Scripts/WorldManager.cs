using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Class contains the settings for the world 
//EG: What the values are for each level in the fishing rod upgrades
public class WorldManager : MonoBehaviour
{
    public static WorldManager wm;
    [Header("Fishing Minigame")]
    public float FishSpawnRange;
    public float[] fishRarityPrice;
    public Sprite[] FishSprites;

    [Header("Fishing Rod Upgrades")]
    public float StartingDepth;
    public float DepthPerLevel;
    public float StartingCapacity;
    public float CapacityPerLevel;
    public float StartingPower;
    public float PowerPerLevel;

    public bool inOverworld;
    public bool inMenu;

    [Header("Animation Settings")]
    public float framesPerSecond;
    public int currentFrame;

    float time;

    void Awake()
    {
        wm = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        currentFrame = Mathf.FloorToInt(time * framesPerSecond);
    }

    public float GetDepth(float Level)
    {
        return StartingDepth + DepthPerLevel * Level;
    }
    public float GetCapacity(float Level)
    {
        return StartingCapacity + CapacityPerLevel * Level;
    }
    public float GetPower(float Level)
    {
        return StartingPower + PowerPerLevel * Level;
    }
}

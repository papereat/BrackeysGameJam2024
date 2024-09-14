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

    [Header("Fishing Rod Upgrades")]
    public float[] Depth;
    public float[] Capacity;
    public float[] Power;

    public bool inOverworld;

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
}

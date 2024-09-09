using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Class contains the settings for the world 
//EG: What the values are for each level in the fishing rod upgrades
public class WorldManager : MonoBehaviour
{
    public static WorldManager wm;

    [Header("Fishing Rod Upgrades")]
    public float[] Depth;
    public float[] Capacity;
    public float[] Power;

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

    }
}

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnController : MonoBehaviour
{
    public static EnemySpawnController enemySpawnController;
    WorldManager worldManager;

    public GameObject enemyPrefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public GameObject storage;

    public GameObject GeneralUI;

    public int waveCounter;
    public bool waveButtonPressed;

    public float startingEnemies;
    public float startingEnemiesPerWave;
    public float startingEnemiesPerWaveMultiplier;
    
    void Start()
    {
        WorldManager worldManager = WorldManager.wm;
        SpawnEnemies();
    }
    void Awake()
    {
        enemySpawnController = this;
    }

    void Update()
    {
        if(storage.transform.childCount == 0)
        {
            GeneralUI.transform.GetChild(2).gameObject.SetActive(true);
            waveButtonPressed = false;
        }
        else
        {
            GeneralUI.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
    public void newWave()
    {
        if(!waveButtonPressed)
        {
            waveButtonPressed = true;
            waveCounter++;

            GeneralUI.transform.GetChild(1).GetComponent<TMP_Text>().text = "Round " + waveCounter;

            SpawnEnemies();
        }
    }
    void SpawnEnemies()
    {
        float amount = startingEnemies * Mathf.Pow(startingEnemiesPerWaveMultiplier, (startingEnemiesPerWave * waveCounter));
        
        for(int i = 0; i < amount; i++)
        {
            GameObject newGuy = Instantiate(enemyPrefab, Random.Range(0, 2) == 1 ? spawnPoint1.transform.position:spawnPoint2.transform.position, Quaternion.identity, storage.transform);
            
            newGuy.GetComponent<Enemies>().id = UnityEngine.Random.Range(-2147483648, 2147483647);
        }

    }
}

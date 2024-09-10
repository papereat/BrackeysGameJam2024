using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //This bascily makes it so that in you can just do PlayerManager.player to get a refercne to the player object
    public static PlayerManager player;



    //These are the stats that change over time like money and day and time
    [Header("Stats")]
    public float Money;
    public float valueOnShip;
    public int day;
    public float time;
    public FishingRod fishingRod;




    //These are refernces to other objects in the scene
    [Header("Refernces")]
    public ShipMovement shipMovement;
    public FishingMinigameControler FMC;




    public enum PlayerState
    {
        Boat,
        Fishing
    };

    [Header("State")]
    public PlayerState playerState;


    Camera cam;

    Vector3 camera_watch_positon;

    void Awake()
    {
        player = this;
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shipMovement.EveryFrame();
        
        //Code Due to State
        if (playerState == PlayerState.Boat)
        {
            BoatUpdate();
        }
        else if (playerState == PlayerState.Fishing)
        {
            FishingUpdate();
        }

        //Camera Movement
        CameraMovement();
    }

    void CameraMovement()
    {
        cam.transform.position = new Vector3(camera_watch_positon.x, camera_watch_positon.y + 3.25f, -10);
    }

    //Runs every frame while in the boat state
    void BoatUpdate()
    {
        
        //Ship Movement
        shipMovement.OnFrame();


        //Camera movemenmt
        camera_watch_positon = shipMovement.GetShipPosition();


    }

    //Runs every frame while fishing
    void FishingUpdate()
    {
        //Hook and other movement
        FMC.OnFrame();

        //Camera Movement
        camera_watch_positon = FMC.GetCameraPostion();
    }


    public Vector3 GetCurrentPosition()
    {
        if (playerState == PlayerState.Boat)
        {
            return shipMovement.GetShipPosition();
        }
        else if (playerState == PlayerState.Fishing)
        {
            return FMC.GetPosition();
        }


        return new Vector3();
    }


    public void StartFishing()
    {
        playerState = PlayerState.Fishing;
        FMC.ResetMinigame();
    }

    public void StopFishing()
    {
        playerState = PlayerState.Boat;
    }
}


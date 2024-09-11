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
    public Transform CameraFallowObject;




    public enum PlayerState
    {
        Boat,
        Fishing
    };

    [Header("State")]
    public PlayerState playerState;



    Vector3 camera_watch_positon;

    void Awake()
    {
        player = this;
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
        CameraFallowObject.position = new Vector2(camera_watch_positon.x, camera_watch_positon.y + 3.25f);
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

        shipMovement.GetComponent<SpriteManager>().current_sprite = 1;
    }

    public void StopFishing()
    {
        playerState = PlayerState.Boat;
        shipMovement.GetComponent<SpriteManager>().current_sprite = 0;
    }
}


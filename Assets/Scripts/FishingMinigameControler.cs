using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class FishingMinigameControler : MonoBehaviour
{
    public bool OnSurface;
    public bool GoingDown;
    public bool GoingUp;

    public KeyCode GoUpKey = KeyCode.Space;
    public KeyCode FishKey = KeyCode.Mouse0;

    public float GoingDownSpeed;
    public float GoingUpSpeed;


    public GameObject FishCollection;
    public GameObject FishPrefab;

    PlayerManager player;
    Rigidbody2D rb;
    WorldManager wm;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.player;
        wm = WorldManager.wm;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    public void ResetMinigame()
    {
        GoingDown = false;
        GoingUp = false;
        OnSurface = true;

        //Curretnly Telaporting the hook to the ship whenever we start fishing
        //We should eventually replace this with smth better

    }
    public void OnFrame()
    {
        if (OnSurface)
        {
            OnSurfaceCode();
        }
        else if (GoingDown)
        {
            GoingDownCode();
        }
        else if (GoingUp)
        {
            GoingUpCode();
        }
    }

    public void OnSurfaceCode()
    {
        transform.position = player.shipMovement.GetShipPosition();
        //This is where the click and hold to try to get the thing in the bar minigame would be

        //For now I am going to skip this by going donw on click
        if (Input.GetKeyDown(FishKey))
        {
            OnSurface = false;
            GoingDown = true;
        }


    }

    public void GoingDownCode()
    {
        //Should Spawn Fish here
        //We want to Span the fish a bit under where the camera ends at random points while going down and in random x location within a range around the hook


        //Checks if current depth has exceded or is equal to max depth of the rod
        if (transform.position.y * -1 >= wm.Depth[player.fishingRod.Depth])
        {
            //Has hit max depth and will not start going up
            GoingDown = false;
            GoingUp = true;
            rb.velocity = new Vector2();

            return;
        }

        //has not hit bottom
        rb.velocity = new Vector2(0, -1) * GoingDownSpeed;


    }

    public void GoingUpCode()
    {
        //Checks if at surface
        if (transform.position.y >= 0)
        {
            //on surface
            OnSurface = true;
            GoingUp = false;
            rb.velocity = new Vector2();

            return;
        }

        //has not reached top yet

        //Move Up
        rb.velocity = new Vector2(0, 1) * GoingUpSpeed;

        //Presses Key To Speeed Up Going Up
        if (Input.GetKeyDown(GoUpKey))
        {
            //Eventually replace this with an animation of the hook going toward the middle and going to the ship
            OnSurface = true;
            GoingUp = false;
            rb.velocity = new Vector2();

            return;
        }
    }

    public Vector3 GetCameraPostion()
    {
        return transform.position;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }


}

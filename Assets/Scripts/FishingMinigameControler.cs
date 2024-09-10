using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
//using Unity.Mathematics;
using UnityEngine;

public class FishingMinigameControler : MonoBehaviour
{
    [Header("Fish Spawning")]
    public float FishSpawnDepth;
    public float BaseFishSpawnChance;
    public float FishSpawnChancePerUnit;
    public Transform FishCollection;
    public GameObject FishPrefab;

    [Header("State")]
    public bool OnSurface;
    public bool GoingDown;
    public bool GoingUp;
    public int FishCollected;
    public float fishValue;

    [Header("other")]


    public KeyCode GoUpKey = KeyCode.Space;
    public KeyCode FishKey = KeyCode.Mouse0;

    public float GoingDownTime;
    public float GoingUpSpeed;







    PlayerManager player;
    Rigidbody2D rb;
    WorldManager wm;

    float x_location;
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
        x_location = transform.position.x;

        //
        FishCollected = 0;
        player.valueOnShip += fishValue;
        fishValue = 0;

        //Delete any fish
        for (int i = 0; i < FishCollection.childCount; i++)
        {
            Destroy(FishCollection.GetChild(i).gameObject);
        }
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
        SpawnFish();


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
        rb.velocity = new Vector2(0, -1) * wm.Depth[player.fishingRod.Depth] / GoingDownTime;


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

        //Move to mouse position
        //Eventually we will repalce this with smth smother prob a lerp
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);

        //Move Up
        rb.velocity = new Vector2(0, 1) * GoingUpSpeed;

        //Presses Key To Speeed Up Going Up
        if (Input.GetKeyDown(GoUpKey) || atCapacity())
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
        return new Vector3(x_location, transform.position.y, transform.position.z);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    void SpawnFish()
    {
        //Calculate Fish Spawn Chance
        float spawnChance = BaseFishSpawnChance + FishSpawnChancePerUnit * Mathf.Abs(transform.position.y);

        //spawn fish at level
        if (Random.Range(0f, 1f) <= spawnChance * Time.deltaTime)
        {
            GameObject spawnedFish = Instantiate(FishPrefab, new Vector3(transform.position.x + Random.Range(-1f, 1f) * wm.FishSpawnRange, transform.position.y - FishSpawnDepth, 0), new Quaternion(), FishCollection);
            spawnedFish.GetComponent<FishControler>().CreateStats(Mathf.Abs(transform.position.y));
        }
    }

    public void CollectFish(FishControler fish)
    {
        if (!atCapacity())
        {
            //Collect fish
            FishCollected += 1;
            fishValue += fish.value;

            //For now we will just delte the fish
            Destroy(fish.gameObject);
        }
        //does not collect fish
    }

    bool atCapacity()
    {
        return FishCollected >= wm.Capacity[player.fishingRod.Capacity];
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishControler : MonoBehaviour
{
    Collider2D this_collider;

    PlayerManager player;
    public WorldManager wm;
    Rigidbody2D rb;

    public float value;
    public int rarityType;

    public float FishSpeed;
    public bool GoingLeft;
    float natural_x;

    void Awake()
    {
        this_collider = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.player;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        //Checks if the center of the player is inside the bounds of the collider
        if (player.playerState == PlayerManager.PlayerState.Fishing && player.FMC.GoingUp && this_collider.bounds.Contains(player.GetCurrentPosition()))
        {
            player.FMC.CollectFish(this);
        }

        //Fish Movement
        //Turn around if on edge of swimming range
        if ((transform.position.x - natural_x) * (GoingLeft ? -1 : 1) >= wm.FishSpawnRange)
        {
            GoingLeft = !GoingLeft;
        }
        //move
        rb.velocity = new Vector2(GoingLeft ? -1 : 1, 0) * FishSpeed;

        GetComponent<SpriteRenderer>().flipX = !GoingLeft;



    }

    public void CreateStats(float depth)
    {
        wm = WorldManager.wm;
        //I set value to whole number, change later
        rarityType = Random.Range(0, 4);
        value = Mathf.Round(depth * wm.fishRarityPrice[rarityType]);

        //value = depth;
        natural_x = transform.position.x;
    }
}

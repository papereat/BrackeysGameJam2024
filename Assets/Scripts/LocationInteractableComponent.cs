using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInteractableComponent : MonoBehaviour
{
    public Collider2D this_collider;
    [SerializeField]
    KeyCode Interact_Keycode = KeyCode.Space;

    public PlayerManager player;
    public WorldManager worldManager;
    public UnderworldControler underworldControler;

    void Awake()
    {
        this_collider = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.player;
        worldManager = WorldManager.wm;
    }

    // Update is called once per frame
    public virtual void Update()
    {

        if (worldManager.inOverworld)
        {
            //Checks if the center of the player is inside the bounds of the collider
            if (this_collider.bounds.Contains(PlayerManager.player.shipMovement.GetShipPosition()))
            {
                //Add Checks to make sure player can interact with this object

                //Checks for Player Input
                if (Input.GetKeyDown(Interact_Keycode))
                {
                    OnActivate();
                }

                OnFrame();
            }
        }
        else
        {
            if (this_collider.bounds.Contains(underworldControler.transform.position))
            {
                //Add Checks to make sure player can interact with this object
                //Checks for Player Input
                if (Input.GetKeyDown(Interact_Keycode))
                {
                    OnActivate();
                }

                OnFrame();
            }
        }

    }


    //This Function Runs When the Players Collider is tuching the collider of this location Interactable
    void OnTriggerStay2D(Collider2D collider)
    {


    }

    //Yousef - Unsure how else to use Update in ShopInteractible so I made and ovverided this
    public virtual void OnFrame()
    {

    }

    //This Function can be replaced in its children to activate certian things
    //For Example
    //For the upgrade shop you would make a child of this script and replace the function bellow to open the shop UI
    public virtual void OnActivate()
    {
        Debug.Log("test");
    }
}

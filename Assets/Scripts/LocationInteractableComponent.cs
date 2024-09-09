using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInteractableComponent : MonoBehaviour
{
    Collider2D this_collider;
    [SerializeField]
    KeyCode Interact_Keycode = KeyCode.Space;
    [SerializeField]

    bool interact = false;

    void Awake()
    {
        this_collider = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        //Checks if the center of the player is inside the bounds of the collider
        if (this_collider.bounds.Contains(PlayerManager.player.GetCurrentPosition()))
        {
            //Add Checks to make sure player can interact with this object

            //Checks for Player Input
            if (Input.GetKeyDown(Interact_Keycode))
            {
                OnActivate();
            }
        }
    }


    //This Function Runs When the Players Collider is tuching the collider of this location Interactable
    void OnTriggerStay2D(Collider2D collider)
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

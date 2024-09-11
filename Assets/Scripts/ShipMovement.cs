using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] KeyCode Left = KeyCode.A;
    [SerializeField] KeyCode Right = KeyCode.D;

    public GameObject VFX;

    //Refernce to the Rigidbody
    Rigidbody2D rb;

    //Awake Functions Run Before the start function
    //Only use awake function for setting references and controling things inside this object
    //Dont try to use functions in other objects cus they wont work in awake instead use Start
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Runs Everyframe while boat
    public void OnFrame()
    {

        Vector2 mov = new Vector2();
        if (Input.GetKey(Left))
        {
            mov.x -= 1;

        }
        if (Input.GetKey(Right))
        {
            mov.x += 1;

        }



        rb.velocity = mov * movementSpeed;

        if (mov.x == 1)
        {
            VFX.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (mov.x == -1)
        {
            VFX.GetComponent<SpriteRenderer>().flipX = false;
        }



    }

    public void EveryFrame()
    {
        rb.velocity = new Vector2();
    }

    public Vector3 GetShipPosition()
    {
        return transform.position;
    }
}

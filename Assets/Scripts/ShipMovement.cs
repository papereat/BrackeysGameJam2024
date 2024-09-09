using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] KeyCode Left = KeyCode.A;
    [SerializeField] KeyCode Right = KeyCode.D;

    public bool can_move;

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
        if (can_move)
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
        }


    }

    public Vector3 GetShipPosition()
    {
        return transform.position;
    }
}

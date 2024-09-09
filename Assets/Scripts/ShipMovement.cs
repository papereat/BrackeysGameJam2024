using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] KeyCode Left;
    [SerializeField] KeyCode Right; 

    void Update()
    {
        if(!Input.GetKey(Left) && !Input.GetKeyDown(Right))
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);     
        }

        if(Input.GetKey(Left))
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }
        if(Input.GetKey(Right))
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);     
        }

    }
}

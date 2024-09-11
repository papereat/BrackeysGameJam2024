using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float Health = 50;


    public void Damage(float DamageAmount)
    {
        Health -= DamageAmount;

        Debug.Log(gameObject.name + " Got Damaged");
    }
}

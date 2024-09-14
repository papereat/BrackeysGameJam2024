using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    SoundController soundController;

    [SerializeField] float movementSpeed = 1f;
    [SerializeField] KeyCode Left = KeyCode.A;
    [SerializeField] KeyCode Right = KeyCode.D;

    public GameObject VFX;

    //Refernce to the Rigidbody
    Rigidbody2D rb;

    float movingIncrease;
    public float changeTime;
    public Vector2 boatSoundRange;

    //Awake Functions Run Before the start function
    //Only use awake function for setting references and controling things inside this object
    //Dont try to use functions in other objects cus they wont work in awake instead use Start
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        soundController = SoundController.soundController;

        StartCoroutine(BoatSounds(0));
    }

    IEnumerator BoatSounds(float time)
    {
        yield return new WaitForSeconds(time);

        soundController.playBoatSound(10, 1);
        StartCoroutine(BoatSounds(soundController.sounds[10].length));
    }

    //Runs Everyframe while boat
    public void OnFrame()
    {
        soundController.boatStateVolume = 0.1f;
        Vector2 mov = new Vector2();

        if (Input.GetKey(Left))
        {
            movingIncrease += Time.deltaTime / changeTime;

            mov.x -= 1;
        }
        if (Input.GetKey(Right))
        {
            movingIncrease += Time.deltaTime / changeTime;

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
        else
        {
            movingIncrease -= Time.deltaTime / changeTime;
        }

        movingIncrease = Mathf.Clamp(movingIncrease, 0, 1);

        soundController.boatStateVolume = Mathf.Lerp(boatSoundRange.x, boatSoundRange.y, movingIncrease);

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

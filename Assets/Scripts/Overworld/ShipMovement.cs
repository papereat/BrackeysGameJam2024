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

    public ParticleSystem boatParticles;
    public Vector3 ParticleDisplacment;
    public Vector3 ParticleAngle;

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


        rb.velocity = new Vector2(mov.x * movementSpeed, rb.velocity.y);

        var emission = boatParticles.emission;
        if (mov.x == 1)
        {
            VFX.GetComponent<SpriteRenderer>().flipX = true;
            boatParticles.transform.localPosition = new Vector3(-ParticleDisplacment.x, ParticleDisplacment.y, ParticleDisplacment.z);
            boatParticles.transform.localEulerAngles = new Vector3(ParticleAngle.x, -ParticleAngle.y, ParticleAngle.z);

            emission.enabled = true;
        }
        else if (mov.x == -1)
        {
            VFX.GetComponent<SpriteRenderer>().flipX = false;
            emission.enabled = true;
            boatParticles.transform.localPosition = ParticleDisplacment;
            boatParticles.transform.localEulerAngles = ParticleAngle;
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
        var emission = boatParticles.emission;
        emission.enabled = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public Vector3 GetShipPosition()
    {
        return transform.position;
    }
}

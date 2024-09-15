using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCyleControler : MonoBehaviour
{
    public static DayCyleControler dcc;
    SoundController soundController;
    public GameObject ShopUI;
    public GameObject GeneralUI;
    public float DayLength;
    public float time;

    [Header("Hole Settings")]
    public float OpenHoleTime;
    public int OpenHoleDay;
    public Vector2 HoleSize;
    public Vector2 timeToFillHole;
    public float timeTillStartFadeToBlack;
    public float FadeToBlackTime;
    public float timeInFadeToBlackBeforeScreenTransition;
    public float timeTillGravity;
    public float Gravity;
    public float fadedBlack;


    public Material waterMaterial;
    public Image FadeToBlackImage;

    bool holeOpened = false;
    bool endingday = false;

    SaveManager sm;
    PlayerManager player;
    WorldManager wm;
    SceneControler sceneControler;

    void Awake()
    {
        dcc = this;
    }

    void Start()
    {
        sm = SaveManager.sm;
        player = PlayerManager.player;
        wm = WorldManager.wm;
        sceneControler = SceneControler.sc;
        soundController = SoundController.soundController;

        ResetHole();
    }

    // Update is called once per frame
    void Update()
    {
        //continue time
        time += Time.deltaTime;


        //End day
        if (time >= DayLength && !endingday)
        {
            EndDay();
        }

        //Open Whole
        if (wm.inOverworld)
        {
            if (player.day >= OpenHoleDay & time >= OpenHoleTime & !holeOpened)
            {
                OpenBigHole();
            }

        }
    }

    void OpenBigHole()
    {
        holeOpened = true;



        //Tell Player To Stop Moving
        NoMovePlayer();

        waterMaterial.SetFloat("_Hole_Position", player.GetCurrentPosition().x);

        //Water To start Opening Up the Water
        StartCoroutine(WaterOpen());

        //Start Coroutine to wait a few seconds then make player fall
        StartCoroutine(PlayerFall());


        //Start coroutine to wait a ffew seconds then start fade to black and once faded to black switch scene
        StartCoroutine(FadeToBlackHell());

    }

    void ResetHole()
    {
        waterMaterial.SetVector("_Hole_Size", Vector2.zero);
    }

    void NoMovePlayer()
    {
        player.StartHoleEffect();
    }

    IEnumerator WaterOpen()
    {
        float time = 0;
        while (true)
        {
            waterMaterial.SetVector("_Hole_Size", new Vector2(Mathf.Lerp(0, HoleSize.x, time / timeToFillHole.x), Mathf.Lerp(0, HoleSize.y, time / timeToFillHole.y)));



            if (time >= Mathf.Max(timeToFillHole.x, timeToFillHole.y))
            {
                break;
            }

            time += 0.1f;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator PlayerFall()
    {
        yield return new WaitForSeconds(timeTillGravity);
        player.shipMovement.GetComponent<Rigidbody2D>().gravityScale = Gravity;
    }

    IEnumerator FadeToBlackHell()
    {
        yield return new WaitForSeconds(timeTillStartFadeToBlack);
        float time = 0;
        while (true)
        {
            FadeToBlackImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, time / FadeToBlackTime));

            if (time >= FadeToBlackTime)
            {
                break;
            }

            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(timeInFadeToBlackBeforeScreenTransition);
        sm.CollectData();
        sm.WriteData();
        sceneControler.LoadScene(2);
    }

    IEnumerator FadeToBlack()
    {
        sm.CollectData();
        sm.WriteData();
        float time = 0;
        while (true)
        {
            FadeToBlackImage.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, time / FadeToBlackTime));

            if (time >= FadeToBlackTime)
            {
                break;
            }

            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(timeInFadeToBlackBeforeScreenTransition);

        ResetDay();
        time = 0;
        while (true)
        {
            FadeToBlackImage.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, time / FadeToBlackTime));

            if (time >= FadeToBlackTime)
            {
                break;
            }

            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        FadeToBlackImage.color = new Color(0, 0, 0, 0);

    }
    void ResetDay()
    {
        Debug.Log("Worked?");

        player.day++;

        soundController.playerOverStateVolume = 1f;
        soundController.playerUnderStateVolume = 0f;
        soundController.boatStateVolume = 1;

        player.valueOnShip = 0f;

        time = 0;
        player.playerState = PlayerManager.PlayerState.Boat;
        player.shipMovement.transform.position = Vector3.zero;
        player.FMC.GoingDown = false;
        player.FMC.GoingUp = false;
        player.FMC.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.FMC.OnSurface = true;

        endingday = false;
        sm.CollectData();
        sm.WriteData();
    }

    void PromptEndDay()
    {
        //Temp Currently Does nothing
        //We want it to make player stop bring hook to surface and a UI pops up telling player that the day has ended and to press the end day button
    }

    public void EndDay()
    {
        endingday = true;

        ShopUI.GetComponent<Canvas>().enabled = false;

        GeneralUI.GetComponent<Canvas>().enabled = true;

        StartCoroutine(FadeToBlack());

        //Do stuff to finish day



        //Save Game


        //Fade To black



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCyleControler : MonoBehaviour
{
    public static DayCyleControler dcc;
    public float DayLength;
    public float time;

    [Header("Hole Settings")]
    public float OpenHoleTime;
    public int OpenHoleDay;
    public Vector2 HoleSize;
    public Vector2 timeToFillHole;


    public Material waterMaterial;

    bool holeOpened = false;

    SaveManager sm;
    PlayerManager player;
    WorldManager wm;

    void Awake()
    {
        dcc = this;
    }

    void Start()
    {
        sm = SaveManager.sm;
        player = PlayerManager.player;
        wm = WorldManager.wm;

        ResetHole();
    }

    // Update is called once per frame
    void Update()
    {
        //continue time
        time += Time.deltaTime;


        //End day
        if (time >= DayLength)
        {
            PromptEndDay();
        }

        //Open Whole
        if (wm.inOverworld & player.day >= OpenHoleDay & time >= OpenHoleTime & !holeOpened)
        {
            OpenBigHole();
        }
    }

    void OpenBigHole()
    {
        holeOpened = true;

        waterMaterial.SetFloat("_Hole_Position", player.);

        //Tell Player To Stop Moving
        NoMovePlayer();

        //Water To start Opening Up the Water
        StartCoroutine(WaterOpen());

        //Start Coroutine to wait a few seconds then make player fall

        //Start coroutine to wait a ffew seconds then start fade to black and once faded to black switch scene

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

    void PlayerFall() { }

    void FadeToBlack() { }

    void PromptEndDay()
    {
        //Temp Currently Does nothing
        //We want it to make player stop bring hook to surface and a UI pops up telling player that the day has ended and to press the end day button
    }

    public void EndDay()
    {
        //Do stuff to finish day
        player.day++;

        time = 0;


        //Save Game
        sm.CollectData();
        sm.WriteData();


    }
}

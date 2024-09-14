using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCyleControler : MonoBehaviour
{
    public static DayCyleControler dcc;
    public float DayLength;
    public float time;

    public float OpenHoleTime;
    public int OpenHoleDay;

    SaveManager sm;
    PlayerManager player;

    void Awake()
    {
        dcc = this;
    }

    void Start()
    {
        sm = SaveManager.sm;
        player = PlayerManager.player;
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

        //if (player.day >= OpenHoleDay & time >= OpenHoleTime)
        //{
        //    OpenBigHole();
        //}
    }

    void OpenBigHole()
    {

    }

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

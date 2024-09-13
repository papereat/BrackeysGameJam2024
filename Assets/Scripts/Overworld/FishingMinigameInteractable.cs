using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigameInteractable : LocationInteractableComponent
{
    public override void OnActivate()
    {
        Debug.Log("tes");
        if (player.playerState == PlayerManager.PlayerState.Boat)
        {
            player.StartFishing();
        }
        else if (player.playerState == PlayerManager.PlayerState.Fishing && player.FMC.OnSurface)
        {
            Debug.Log("test");
            player.StopFishing();
        }
    }
}

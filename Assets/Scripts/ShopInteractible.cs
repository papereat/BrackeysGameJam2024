using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;

public class ShopInteractible : LocationInteractableComponent
{
    public GameObject ShopUI;
    public GameObject GeneralUI;
    
    //Enables/Disables UI
    public override void OnActivate()
    {
        Debug.Log("WORKED");
        ShopUI.GetComponent<Canvas>().enabled = !ShopUI.GetComponent<Canvas>().enabled;
        GeneralUI.GetComponent<Canvas>().enabled = !GeneralUI.GetComponent<Canvas>().enabled;
    }

    //Sets Money to Money Amount
    public override void OnFrame()
    {

    }

    //PLACEHOLDER
    public void sellFish()
    {
        //Change later to depend on type of fish etc. etc.
        player.Money += player.valueOnShip;
        player.valueOnShip = 0;
        
        ShopUI.transform.GetChild(3).GetComponent<TMP_Text>().text = "Money: " + player.Money;
        GeneralUI.transform.GetChild(0).GetComponent<TMP_Text>().text = "Money: " + player.Money;
    }

    //Changing Value based on button press
    public void upgradeDepth()
    {
        if(player.fishingRod.Depth < 5)
        {
            player.fishingRod.Depth += 1;
            ShopUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Level: " + player.fishingRod.Depth;
        }
        else
        {
            ShopUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Max Level";
        }

    }

    public void upgradePower()
    {
        if(player.fishingRod.Power < 5)
        {
            player.fishingRod.Power += 1;
            ShopUI.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Level: " + player.fishingRod.Power;
        }
        else
        {
            ShopUI.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Max Level";
        }
        
    }

    public void upgradeCapacity()
    {
        if(player.fishingRod.Capacity < 5)
        {
            player.fishingRod.Capacity += 1;
            ShopUI.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Level: " + player.fishingRod.Capacity;
        }
        else
        {
            ShopUI.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Max Level";
        }
    }
}
